using BoardGameNight.Migrations;
using BoardGameNight.Models;
using BoardGameNight.Repositories;
using BoardGameNight.Repositories.Implementations;
using BoardGameNight.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
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
    [Authorize]

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var age = user?.GetAge() ?? 0;

        ViewData["UserAge"] = age;
        
        ViewData["UserName"] = user?.UserName ?? string.Empty; 
        ViewData["UserId"] = user?.Id ?? string.Empty;

        return View(await _repo.GetAllAsync());
    }
    // GET: Bordspellenavond/Create
    [Authorize(policy : "MinimumAge")]
    public async Task<IActionResult> Create()
    {
        ViewBag.Bordspellen = await _bordspellenRepository.GetAllAsync();
        return View();
    }

    // POST: Bordspellenavond/Create
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(policy : "MinimumAge")]
    public async Task<IActionResult> Create(Bordspellenavond bordspellenavond, 
        List<int> selectedBordspellen, 
        List<int> selectedDietaryRequirements, 
        List<int> selectedDrankVoorkeur) // changed from selectedDrinks
    {
        if (ModelState.IsValid)
        {
            
            var userId = _userManager.GetUserId(User);
            var organisator = await _userManager.FindByIdAsync(userId);
            if (organisator.GetAge() < 18)
            {
                ModelState.AddModelError("AgeError",
                    "Je moet 18 jaar of ouder zijn om een bordspellenavond te organiseren.");
            }


            bordspellenavond.Organisator = organisator;

            // Add selected board games to bordspellenavond
            foreach (var id in selectedBordspellen)
            {
                var bordspel = await _bordspellenRepository.GetByIdAsync(id);
                bordspellenavond.Bordspellen.Add(bordspel);
            }

            // If no dietary requirements are selected, select a default one
            if (!selectedDietaryRequirements.Any())
            {
                selectedDietaryRequirements.Add(0); // Replace with your default dietary requirement value
            }

            // Convert selected dietary requirements to Dieetwensen enum value
            bordspellenavond.Dieetwensen = (Dieetwensen)selectedDietaryRequirements
                .Aggregate(0, (current, requirement) => current | requirement);

            // Convert selected drink preferences to DrankVoorkeur enum value
            bordspellenavond.DrankVoorkeur = (DrankVoorkeur)selectedDrankVoorkeur
                .Aggregate(0, (current, drink) => current | drink);

            await _repo.CreateAsync(bordspellenavond, userId);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Bordspellen = await _bordspellenRepository.GetAllAsync();
        ViewBag.DietaryRequirements = Enum.GetValues(typeof(Dieetwensen));

        return View(bordspellenavond);
    }
// GET: Bordspellenavond/Edit/5
    [HttpGet("edit/{id:int}")]
    [Authorize(policy : "MinimumAge")]
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

        ViewBag.Bordspellen = await _bordspellenRepository.GetAllAsync();
        ViewBag.DietaryRequirements = Enum.GetValues(typeof(Dieetwensen));

        return View(bordspellenavond);
    }

    // POST: Bordspellenavond/Edit/5
[HttpPost("edit/{id:int}")]
[ValidateAntiForgeryToken]
[Authorize(policy : "MinimumAge")]
public async Task<IActionResult> Edit(int id, Bordspellenavond bordspellenavond, 
    List<int> selectedBordspellen, 
    List<int> selectedDietaryRequirements, 
    List<int> selectedDrankVoorkeur)
{
    if (id != bordspellenavond.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        // Load the existing entity from the database
        var existingBordspellenavond = await _repo.GetByIdAsync(id);

        if (existingBordspellenavond == null)
        {
            return NotFound();
        }

        // Update only the fields that are being edited
        existingBordspellenavond.Adres = bordspellenavond.Adres;
        existingBordspellenavond.MaxAantalSpelers = bordspellenavond.MaxAantalSpelers;
        existingBordspellenavond.DatumTijd = bordspellenavond.DatumTijd;
        existingBordspellenavond.Is18Plus = bordspellenavond.Is18Plus;

        // Update selected board games
        var currentGameIds = existingBordspellenavond.Bordspellen.Select(b => b.Id).ToList();

        var addedGames = selectedBordspellen.Except(currentGameIds).ToList();
        var removedGames = currentGameIds.Except(selectedBordspellen).ToList();

        foreach (var removedGameId in removedGames)
        {
            var removedGame = existingBordspellenavond.Bordspellen.FirstOrDefault(g => g.Id == removedGameId);
            if (removedGame != null)
            {
                existingBordspellenavond.Bordspellen.Remove(removedGame);
            }
        }

        foreach (var addedGameId in addedGames)
        {
            var addedGame = await _bordspellenRepository.GetByIdAsync(addedGameId);
            if (addedGame != null)
            {
                existingBordspellenavond.Bordspellen.Add(addedGame);
            }
        }
        // If no dietary requirements are selected, select a default one
        if (!selectedDietaryRequirements.Any())
        {
            selectedDietaryRequirements.Add(0); // Replace with your default dietary requirement value
        }

        // Update dietary requirements and drink preferences
        existingBordspellenavond.Dieetwensen = (Dieetwensen)selectedDietaryRequirements
            .Aggregate(0, (current, requirement) => current | requirement);
        existingBordspellenavond.DrankVoorkeur = (DrankVoorkeur)selectedDrankVoorkeur
            .Aggregate(0, (current, drink) => current | drink);

        await _repo.UpdateAsync(existingBordspellenavond);
        return RedirectToAction(nameof(Index));
    }

    return View(bordspellenavond);
}
    
    // GET: Bordspellenavond/Details/5
    [Authorize]

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
    [Authorize(policy : "MinimumAge")]
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
    [Authorize(policy : "MinimumAge")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repo.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
    
    // POST: Bordspellenavond/Subscribe/5
    [HttpPost("subscribe/{id:int}")]
    [Authorize]

    public async Task<IActionResult> Subscribe(int id)
    {
        try
        {
            // Get the Bordspellenavond
            var bordspellenavond = await _repo.GetByIdAsync(id);
            if (bordspellenavond == null)
            {
                return NotFound();
            }

            // Get the current user
            var userId = _userManager.GetUserId(User);
            var gebruiker = await _userManager.FindByIdAsync(userId);

            if (gebruiker == null)
            {
                return NotFound();
            }

            // Check if the user is not the Organisator of the Bordspellenavond
            if (bordspellenavond.Organisator.Id == gebruiker.Id)
            {
                ModelState.AddModelError("SubscriptionError",
                    "Je mag je niet inschrijven voor je eigen bordspellenavond.");
                return View("Error"); // Replace with your error view
            }

            // Check if the user is already subscribed
            if (bordspellenavond.Deelnemers.Any(d => d.Id == gebruiker.Id))
            {
                ModelState.AddModelError("SubscriptionError", "Je bent al ingeschreven voor deze bordspellenavond.");
                return View("Error"); // Replace with your error view
            }

            // Subscribe the user to the Bordspellenavond
            bordspellenavond.Deelnemers.Add(gebruiker);

            // Save changes
            await _repo.UpdateAsync(bordspellenavond);

            // Redirect to the Bordspellenavond details page
            return RedirectToAction("Details", new { id = bordspellenavond.Id });
        }
        catch (Exception ex)
        {
            // Log the exception


            ModelState.AddModelError("SubscriptionError",
                "Er is een fout opgetreden bij het inschrijven voor de bordspellenavond.");




            return View("Error"); // Replace with your error view
        }
    }

    // POST: Bordspellenavond/Unsubscribe/5
        [HttpPost("unsubscribe/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Unsubscribe(int id)
        {
            try
            {
                // Get the Bordspellenavond
                var bordspellenavond = await _repo.GetByIdAsync(id);
                if (bordspellenavond == null)
                {
                    return NotFound();
                }

                // Get the current user
                var userId = _userManager.GetUserId(User);
                var gebruiker = await _userManager.FindByIdAsync(userId);

                if (gebruiker == null)
                {
                    return NotFound();
                }

                // Check if the user is the Organisator of the Bordspellenavond
                if (bordspellenavond.Organisator.Id == gebruiker.Id)
                {
                    ModelState.AddModelError("UnsubscriptionError", "Organisators kunnen zich niet uitschrijven voor hun eigen bordspellenavond.");
                    return View("Error"); // Replace with your error view
                }

                // Check if the user is not already subscribed
                if (!bordspellenavond.Deelnemers.Any(d => d.Id == gebruiker.Id))
                {
                    ModelState.AddModelError("UnsubscriptionError", "Je bent niet ingeschreven voor deze bordspellenavond.");
                    return View("Error"); // Replace with your error view
                }

                // Unsubscribe the user from the Bordspellenavond
                bordspellenavond.Deelnemers.Remove(gebruiker);

                // Save changes
                await _repo.UpdateAsync(bordspellenavond);

                // Redirect to the Bordspellenavond details page
                return RedirectToAction("Details", new { id = bordspellenavond.Id });
            }
            catch (Exception ex)
            {
                // Log the exception

                ModelState.AddModelError("UnsubscriptionError", "Er is een fout opgetreden bij het uitschrijven voor de bordspellenavond.");
                return View("Error"); // Replace with your error view
            }
        }
    }