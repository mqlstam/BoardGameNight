using BoardGameNight.Data;
using BoardGameNight.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGameNight.Controllers;

public class BordspelController : Controller
{
    private readonly ApplicationDbContext _context;

    public BordspelController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Bordspel
    public async Task<IActionResult> Index()
    {
        return View(await _context.Bordspellen.ToListAsync());
    }

    // GET: Bordspel/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bordspel = await _context.Bordspellen
            .FirstOrDefaultAsync(m => m.Id == id);
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
    public async Task<IActionResult> Create([Bind("Id,Naam,Beschrijving,Genre,Is18Plus,Foto,SoortSpel")] Bordspel bordspel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(bordspel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(bordspel);
    }

    // Andere acties (Edit, Delete)...
}