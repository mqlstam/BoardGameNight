using BoardGameNight.Models;
using BoardGameNight.Repositories;
using BoardGameNight.Repositories.Implementations;
using BoardGameNight.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class BordspellenavondController : Controller
{
    private readonly IBordspellenavondRepository _repo;
    private readonly IBordspelRepository _bordspellenRepository;
    private readonly UserManager<Persoon> _userManager;  // Change this line

    public BordspellenavondController(
        IBordspellenavondRepository repo, 
        IBordspelRepository bordspellenRepository, 
        UserManager<Persoon> userManager)
    {
        _repo = repo;
        _bordspellenRepository = bordspellenRepository;
        _userManager = userManager;
    }
    // GET: Bordspellenavond
    public async Task<IActionResult> Index()
    {
        return View(await _repo.GetAllAsync());
    }

    // GET: Bordspellenavond/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.Bordspellen = await _bordspellenRepository.GetAllAsync();
        return View();
    }

    // POST: Bordspellenavond/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Adres,MaxAantalSpelers,DatumTijd,Is18Plus,Dieetwensen,DrankVoorkeur")] Bordspellenavond bordspellenavond) {
        if (ModelState.IsValid)
        {
            
            var userId = _userManager.GetUserId(User);
            var organisator = await _userManager.FindByIdAsync(userId);
            bordspellenavond.Organisator = organisator;

            
            await _repo.CreateAsync(bordspellenavond, userId);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Bordspellen = await _bordspellenRepository.GetAllAsync();

        return View(bordspellenavond);
    }

    // GET: Bordspellenavond/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bordspellenavond = await _repo.GetByIdAsync(id.Value);
        if (bordspellenavond == null)
        {
            return NotFound();
        }
        return View(bordspellenavond);
    }

    // POST: Bordspellenavond/Edit/5

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Adres,MaxAantalSpelers,DatumTijd,Is18Plus,Dieetwensen,DrankVoorkeur")] Bordspellenavond bordspellenavond) {
        if (id != bordspellenavond.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _repo.UpdateAsync(bordspellenavond);
            return RedirectToAction(nameof(Index));
        }
        return View(bordspellenavond);
    }
    
    // GET: Bordspellenavond/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bordspellenavond = await _repo.GetByIdAsync(id.Value);
        if (bordspellenavond == null)
        {
            return NotFound();
        }

        return View(bordspellenavond);
    }
    

    // GET: Bordspellenavond/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bordspellenavond = await _repo.GetByIdAsync(id.Value);
        if (bordspellenavond == null)
        {
            return NotFound();
        }
        return View(bordspellenavond);
    }

    // POST: Bordspellenavond/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repo.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}