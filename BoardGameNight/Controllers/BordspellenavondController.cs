using BoardGameNight.Models;
using BoardGameNight.Repositories;
using BoardGameNight.Repositories.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class BordspellenavondController : Controller
{
    private readonly IBordspellenavondRepository _repo;
    private readonly IBordspelRepository _bordspellenRepository;
    private readonly IEtenRepository _etenRepository;
    private readonly UserManager<Persoon> _userManager;  // Change this line

    public BordspellenavondController(
        IBordspellenavondRepository repo, 
        IBordspelRepository bordspellenRepository, 
        IEtenRepository etenRepository, 
        UserManager<Persoon> userManager)
    {
        _repo = repo;
        _bordspellenRepository = bordspellenRepository;
        _etenRepository = etenRepository;
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
        ViewBag.Eten = await _etenRepository.GetAllAsync();
        return View();
    }

    // POST: Bordspellenavond/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Adres,MaxAantalSpelers,DatumTijd,Is18Plus")] Bordspellenavond bordspellenavond)
    {
        if (ModelState.IsValid)
        {
            string userId = _userManager.GetUserId(User);
            await _repo.CreateAsync(bordspellenavond, userId);
            return RedirectToAction(nameof(Index));
        }
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
    public async Task<IActionResult> Edit(int id, [Bind("Id,Adres,MaxAantalSpelers,DatumTijd,Is18Plus")] Bordspellenavond bordspellenavond)
    {
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