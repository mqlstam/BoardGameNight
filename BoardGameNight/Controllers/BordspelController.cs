using BoardGameNight.Models;
using Microsoft.AspNetCore.Mvc;

public class BordspelController : Controller
{
    private readonly IBordspelRepository _bordspelRepository;

    public BordspelController(IBordspelRepository bordspelRepository)
    {
        _bordspelRepository = bordspelRepository;
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

    // GET: Bordspel/Create
    public IActionResult Create()
    {
        return View();
    }

// POST: Bordspel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Naam,Beschrijving,Genre,Is18Plus,Foto,SoortSpel")] Bordspel bordspel)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            // Return the view with the model including the validation errors
            return View(bordspel);
        }

        await _bordspelRepository.CreateAsync(bordspel);
        return RedirectToAction(nameof(Index));
    }
}