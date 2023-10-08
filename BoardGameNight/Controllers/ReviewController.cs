using BoardGameNight.Data;
using BoardGameNight.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGameNight.Controllers;

public class ReviewController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReviewController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Review
    public async Task<IActionResult> Index()
    {
        return View(await _context.Reviews.ToListAsync());
    }

    // GET: Review/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var review = await _context.Reviews
            .Include(r => r.Persoon)
            .Include(r => r.Bordspellenavond)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (review == null)
        {
            return NotFound();
        }

        return View(review);
    }

    // GET: Review/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Review/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Score,Tekst,PersoonId,BordspellenavondId")] Review review)
    {
        if (ModelState.IsValid)
        {
            _context.Add(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(review);
    }

    // Further actions (Edit, Delete)...
}