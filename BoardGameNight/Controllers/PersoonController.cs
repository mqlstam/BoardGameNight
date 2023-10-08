using BoardGameNight.Data;
using BoardGameNight.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGameNight.Controllers;

public class PersoonController : Controller
{
    private readonly ApplicationDbContext _context;

    public PersoonController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Persoon
    public async Task<IActionResult> Index()
    {
        return View(await _context.Personen.ToListAsync());
    }

    // GET: Persoon/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var persoon = await _context.Personen
            .FirstOrDefaultAsync(m => m.Id == id);
        if (persoon == null)
        {
            return NotFound();
        }

        return View(persoon);
    }

    // GET: Persoon/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Persoon/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Naam,Email,Geslacht,Adres,Geboortedatum,Dieetwensen,Allergieën")] Persoon persoon)
    {
        if (ModelState.IsValid)
        {
            _context.Add(persoon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(persoon);
    }

    // Verdere acties (Edit, Delete)...
}