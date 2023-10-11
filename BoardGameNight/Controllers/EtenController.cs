using BoardGameNight.Data;
using BoardGameNight.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGameNight.Controllers;

[Authorize]
public class EtenController : Controller
{
    private readonly ApplicationDbContext _context;

    public EtenController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Eten
    public async Task<IActionResult> Index()
    {
        return View(await _context.Eten.ToListAsync());
    }

    // GET: Eten/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var eten = await _context.Eten
            .FirstOrDefaultAsync(m => m.Id == id);
        if (eten == null)
        {
            return NotFound();
        }

        return View(eten);
    }

    // GET: Eten/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Eten/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Naam,Beschrijving,Allergenen,Calorieën")] Eten eten)
    {
        if (ModelState.IsValid)
        {
            _context.Add(eten);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(eten);
    }

    // Verdere acties (Edit, Delete)...
}