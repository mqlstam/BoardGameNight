using BoardGameNight.Data;
using BoardGameNight.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGameNight.Controllers;

public class BordspellenavondController : Controller
{
    private readonly ApplicationDbContext _context;

    public BordspellenavondController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Bordspellenavond
    public async Task<IActionResult> Index()
    {
        return View(await _context.Bordspellenavonden.ToListAsync());
    }

    // GET: Bordspellenavond/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bordspellenavond = await _context.Bordspellenavonden
            .FirstOrDefaultAsync(m => m.Id == id);
        if (bordspellenavond == null)
        {
            return NotFound();
        }

        return View(bordspellenavond);
    }

    // GET: Bordspellenavond/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Bordspellenavond/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Naam,Datum,Locatie")] Bordspellenavond bordspellenavond)
    {
        if (ModelState.IsValid)
        {
            _context.Add(bordspellenavond);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(bordspellenavond);
    }

    // Verdere acties (Edit, Delete)...
}