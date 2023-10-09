using BoardGameNight.Models;
using Microsoft.AspNetCore.Mvc;
using BoardGameNight.Services;

public class BordspelController : Controller
{
    private readonly IBordspelRepository _bordspelRepository;
    private readonly BlobStorageService _blobStorageService;
    private readonly ILogger<BordspelController> _logger;

    public BordspelController(IBordspelRepository bordspelRepository, BlobStorageService blobStorageService,
        ILogger<BordspelController> logger)
    {
        _bordspelRepository = bordspelRepository;
        _blobStorageService = blobStorageService;
        _logger = logger;
    }


    // GET: Bordspel
    public async Task<IActionResult> Index()
    {
        return View(await _bordspelRepository.GetAllAsync());
    }

    // GET: Bordspel/Details/5
    public async Task<IActionResult> Details(int? id)
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

// GET: Bordspel/Edit/5
    public async Task<IActionResult> Edit(int? id)
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Naam,Beschrijving,Genre,Is18Plus,SoortSpel")] Bordspel bordspel, IFormFile foto)
    {
        if (id != bordspel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                if (foto != null && foto.Length > 0)
                {
                    var fotoUrl = await _blobStorageService.UploadImage(foto, "imagesbordspellen");
                    bordspel.FotoUrl = fotoUrl;
                }

                await _bordspelRepository.UpdateAsync(bordspel);
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
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Naam,Beschrijving,Genre,Is18Plus,SoortSpel")] Bordspel bordspel,
        IFormFile foto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (foto != null && foto.Length > 0)
                {
                    var fotoUrl = await _blobStorageService.UploadImage(foto, "imagesbordspellen");
                    bordspel.FotoUrl = fotoUrl;
                }

                await _bordspelRepository.CreateAsync(bordspel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error creating bordspel");
            }
        }

        var errors = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var error in errors)
        {
            _logger.LogError("Model validation error: {0}", error.ErrorMessage);
        }

        return View(bordspel);
    }

    // GET: Bordspel/Delete/5
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