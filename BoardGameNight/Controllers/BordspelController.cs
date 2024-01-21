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
    /// <summary>
    /// Retrieves all board games.
    /// </summary>
    /// <returns>A list of all board games.</returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _bordspelRepository.GetAllAsync());
    }

    // GET: Bordspel/Details/5
    /// <summary>
    /// Retrieves the details of a specific board game by ID.
    /// </summary>
    /// <param name="id">The ID of the board game to retrieve.</param>
    /// <returns>The details of the board game if found; otherwise, NotFound.</returns>
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
    /// <summary>
    /// Presents the form for editing a board game.
    /// </summary>
    /// <param name="id">The ID of the board game to edit.</param>
    /// <returns>The edit view for the board game if found; otherwise, NotFound.</returns>
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

    // POST: Bordspel/Edit/5
    /// <summary>
    /// Processes the submitted form for editing a board game.
    /// </summary>
    /// <param name="id">The ID of the board game to edit.</param>
    /// <param name="bordspel">The board game object with updated information.</param>
    /// <param name="foto">The new photo for the board game if provided.</param>
    /// <returns>Redirects to the index action if successful; otherwise, returns the edit view with validation errors.</returns>
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
                var existingBordspel = await _bordspelRepository.GetByIdAsync(id);
                if (existingBordspel == null)
                {
                    return NotFound();
                }

                existingBordspel.Naam = bordspel.Naam;
                existingBordspel.Beschrijving = bordspel.Beschrijving;
                existingBordspel.GenreId = bordspel.GenreId; // Update GenreId
                existingBordspel.SoortSpelId = bordspel.SoortSpelId; // Update SoortSpelId
                existingBordspel.Is18Plus = bordspel.Is18Plus;

                // Handle image upload
                if (foto != null && foto.Length > 0)
                {
                    var fotoUrl = await _blobStorageService.UploadImage(foto);
                    existingBordspel.FotoUrl = fotoUrl;
                }

                await _bordspelRepository.UpdateAsync(existingBordspel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating bordspel");
            }
        }

        ViewBag.Genres = await _bordspelGenreRepository.GetAllAsync();
        ViewBag.Soorten = await _soortBordspelRepository.GetAllAsync();
        return View(bordspel);
    }


    // GET: Bordspel/Create
    /// <summary>
    /// Presents the form for creating a new board game.
    /// </summary>
    /// <returns>The create view.</returns>
    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        ViewBag.Genres = await _bordspelGenreRepository.GetAllAsync();
        ViewBag.Soorten = await _soortBordspelRepository.GetAllAsync();

        return View();
    }


    // POST: Bordspel/Create
    /// <summary>
    /// Processes the submitted form for creating a new board game.
    /// </summary>
    /// <param name="bordspel">The new board game object to create.</param>
    /// <param name="foto">The photo for the new board game.</param>
    /// <returns>Redirects to the index action if successful; otherwise, returns the create view with validation errors.</returns>
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

                var fotoUrl = await _blobStorageService.UploadImage(foto);
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
    /// <summary>
    /// Presents the confirmation page for deleting a board game.
    /// </summary>
    /// <param name="id">The ID of the board game to delete.</param>
    /// <returns>The delete confirmation view if found; otherwise, NotFound.</returns>
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

    // POST: Bordspel/Delete/5
    /// <summary>
    /// Deletes the board game with the specified ID after confirmation.
    /// </summary>
    /// <param name="id">The ID of the board game to delete.</param>
    /// <returns>Redirects to the index action if successful; otherwise, returns an error message.</returns>
    [HttpPost]
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