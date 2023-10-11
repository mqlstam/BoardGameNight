using BoardGameNight.Models;
using BoardGameNight.Repositories.Implementations;
using BoardGameNight.Repositories.Interfaces;
using BoardGameNight.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameNight.Controllers;

[Authorize]
[Route("bordspel")]
public class BordspelController : Controller
{
    private readonly IBordspelRepository _bordspelRepository;
    private readonly IBordspelGenreRepository _bordspelGenreRepository;
    private readonly ISoortBordspelRepository _soortBordspelRepository;
    private readonly BlobStorageService _blobStorageService;
    private readonly ILogger<BordspelController> _logger;

    public BordspelController(
        IBordspelRepository bordspelRepository,
        IBordspelGenreRepository bordspelGenreRepository,
        ISoortBordspelRepository soortBordspelRepository,
        BlobStorageService blobStorageService,
        ILogger<BordspelController> logger)
    {
        _bordspelRepository = bordspelRepository;
        _bordspelGenreRepository = bordspelGenreRepository;
        _soortBordspelRepository = soortBordspelRepository;
        _blobStorageService = blobStorageService;
        _logger = logger;
    }


    // GET: Bordspel
    public async Task<IActionResult> Index()
    {
        return View(await _bordspelRepository.GetAllAsync());
    }

    // GET: Bordspel/Details/5
    [HttpGet("details/{id:int}")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bordspel = await _bordspelRepository.GetByIdAsync(id.Value);

        return View(bordspel);
    }

    // GET: Bordspel/Edit/5
    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bordspel = await _bordspelRepository.GetByIdAsync(id.Value);

        ViewBag.Genres = await _bordspelGenreRepository.GetAllAsync();
        ViewBag.Soorten = await _soortBordspelRepository.GetAllAsync();

        return View(bordspel);
    }


    [HttpPost("edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Bordspel bordspel, IFormFile foto = null)
    {
        if (id != bordspel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Load the existing entity from the database
                var existingBordspel = await _bordspelRepository.GetByIdAsync(id);

                if (existingBordspel == null)
                {
                    return NotFound();
                }

                // Update only the fields that are being edited
                existingBordspel.Naam = bordspel.Naam;
                existingBordspel.Beschrijving = bordspel.Beschrijving;
                existingBordspel.Genre = bordspel.Genre;
                existingBordspel.Is18Plus = bordspel.Is18Plus;
                existingBordspel.SoortSpel = bordspel.SoortSpel;

                // Only upload new image if a file has been provided
                if (foto != null && foto.Length > 0)
                {
                    var fotoUrl = await _blobStorageService.UploadImage(foto, "imagesbordspellen");
                    existingBordspel.FotoUrl = fotoUrl;
                }

                await _bordspelRepository.UpdateAsync(existingBordspel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error updating bordspel");
            }
        }

        var errors = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var error in errors)
        {
            _logger.LogError("Model validation error: {0}", error.ErrorMessage);
        }

        return View(bordspel);
    }

    // GET: Bordspel/Create
    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        ViewBag.Genres = await _bordspelGenreRepository.GetAllAsync();
        ViewBag.Soorten = await _soortBordspelRepository.GetAllAsync();

        return View();
    }


    // POST: Bordspel/Create
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Bordspel bordspel, IFormFile foto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (foto == null || foto.Length == 0)
                {
                    ModelState.AddModelError("Foto", "The foto field is required.");
                    return View(bordspel);
                }

                var fotoUrl = await _blobStorageService.UploadImage(foto, "imagesbordspellen");
                bordspel.FotoUrl = fotoUrl;

                // Get genre 
                var genre = await _bordspelGenreRepository.GetByIdAsync(bordspel.GenreId);

                // Get soortspel
                var soortSpel = await _soortBordspelRepository.GetByIdAsync(bordspel.SoortSpelId);
                // Assign to navigation properties
                bordspel.Genre = genre;
                bordspel.SoortSpel = soortSpel;

                await _bordspelRepository.CreateAsync(bordspel);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Foto", ex.Message);
                _logger.LogError(ex, "Error uploading image");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,
                    "An error occurred while saving the game. Please try again later.");
                _logger.LogError(ex, "Error creating bordspel");
            }
        }

        var errors = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var error in errors)
        {
            _logger.LogError("Model validation error: {0}", error.ErrorMessage);
        }

        ViewBag.Genres = await _bordspelGenreRepository.GetAllAsync();
        ViewBag.Soorten = await _soortBordspelRepository.GetAllAsync();


        return View(bordspel);
    }

    // GET: Bordspel/Delete/5
    [HttpGet("delete/{id:int}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bordspel = await _bordspelRepository.GetByIdAsync(id.Value);
        if (bordspel == null)
        {
            return NotFound();
        }

        return View(bordspel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var bordspel = await _bordspelRepository.GetByIdAsync(id);
            if (bordspel == null)
            {
                return NotFound($"No game found with id: {id}");
            }


            await _bordspelRepository.DeleteAsync(bordspel.Id);
        }
        catch (Exception ex)
        {
            // Log the exception message
            System.Diagnostics.Debug.WriteLine($"Exception caught in DeleteConfirmed: {ex.Message}");
        }

        return RedirectToAction(nameof(Index));
    }
}