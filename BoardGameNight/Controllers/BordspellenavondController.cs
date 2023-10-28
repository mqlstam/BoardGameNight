using System.Security.Claims;
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
    
    // GET: Bordspellenavond/MyBordspellenavonden
    
    [HttpGet("mybordspellenavonden")]
    [Authorize]
    
    public async Task<IActionResult> MyBordspellenavonden()
    {
        var user = await _userManager.GetUserAsync(User);
        var age = user?.GetAge() ?? 0;

        ViewData["UserAge"] = age;
        
        ViewData["UserName"] = user?.UserName ?? string.Empty; 
        ViewData["UserId"] = user?.Id ?? string.Empty;

        return View(await _repo.GetAllAsync());
        
    }
    
    // GET: Bordspellenavond/Ingeschreven
    
    [HttpGet("ingeschreven")]
    [Authorize]
    
    public async Task<IActionResult> Ingeschreven()
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
        List<int> selectedDrankVoorkeur,
    List<PotluckItem> potluckItems) 

        
    {
        try
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

            foreach (var item in potluckItems)
            {
                bordspellenavond.PotluckItems.Add(item);
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
            
            
            // add potuck
            foreach (var item in potluckItems)
            {
                bordspellenavond.PotluckItems.Add(item);
            }
            
            bordspellenavond.Is18Plus = bordspellenavond.Bordspellen.Any(b => b.Is18Plus);
            if (bordspellenavond.IsPotluck)
            {
                await _repo.CreateAsync(bordspellenavond, userId);
                return RedirectToAction("CreatePotluckItem", new { bordspellenavondId = bordspellenavond.Id });
            }
            await _repo.CreateAsync(bordspellenavond, userId);

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Bordspellen = await _bordspellenRepository.GetAllAsync();
        ViewBag.DietaryRequirements = Enum.GetValues(typeof(Dieetwensen));
        if (!ModelState.IsValid)
        {
            // ModelState is not valid, let's see why
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

            foreach (var error in errors)
            {
                foreach (var subError in error.Errors)
                {
                    Console.WriteLine($"Property: {error.Key} Error: {subError.ErrorMessage}");
                }
            }
        }
        return View(bordspellenavond);
    }
    
    catch (Exception ex)
    {
        // Log the exception
        // You could use a logging framework here, like Serilog or NLog
        Console.WriteLine(ex);

        // Display a general error message
        ModelState.AddModelError("", "An error occurred while trying to create the game night.");

        return View(bordspellenavond);
    }
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
        if (bordspellenavond.Deelnemers.Any())
        {
            // Add error
            TempData["EditError"] = "Cannot edit bordspellenavond with subscribers";

            return RedirectToAction("Index");

        }

        return View(bordspellenavond);
    }

// POST: Bordspellenavond/Edit/5
    [HttpPost("edit/{id:int}")]
    [ValidateAntiForgeryToken]
    [Authorize(policy : "MinimumAge")]
    public async Task<IActionResult> Edit(int id, Bordspellenavond bordspellenavond, 
        List<int> selectedBordspellen, 
        List<int> selectedDietaryRequirements, 
        List<int> selectedDrankVoorkeur,
    List<PotluckItem> potluckItems)  
    
    {
        if (id != bordspellenavond.Id)
        {
            return NotFound();
        }

        // Load the existing entity from the database
        var existingBordspellenavond = await _repo.GetByIdAsync(id);

        if (existingBordspellenavond == null)
        {
            return NotFound();
        }

        // Check if there are any participants
            if(existingBordspellenavond.Deelnemers.Any())
            {
                // Add error
                TempData["EditError"] = "Cannot edit bordspellenavond with subscribers";
    
                // Redirect back to Details
                return RedirectToAction("Details", new {id}); 
            }

        if (ModelState.IsValid)
        {
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
        
        // Update PotluckItems
        existingBordspellenavond.PotluckItems.Clear();
        foreach (var item in potluckItems)
        {
            existingBordspellenavond.PotluckItems.Add(item);
        }

        // Update dietary requirements and drink preferences
        existingBordspellenavond.Dieetwensen = (Dieetwensen)selectedDietaryRequirements
            .Aggregate(0, (current, requirement) => current | requirement);
        existingBordspellenavond.DrankVoorkeur = (DrankVoorkeur)selectedDrankVoorkeur
            .Aggregate(0, (current, drink) => current | drink);
        
        existingBordspellenavond.Is18Plus = existingBordspellenavond.Bordspellen.Any(b => b.Is18Plus);
        if (bordspellenavond.IsPotluck)
        {
            await _repo.UpdateAsync(existingBordspellenavond);
            return RedirectToAction("CreatePotluckItem", new { bordspellenavondId = existingBordspellenavond.Id });
        }
        return RedirectToAction(nameof(Index));
        }
        await _repo.UpdateAsync(existingBordspellenavond);


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

        // Check if there are any participants
        if(bordspellenavond.Deelnemers.Any())
        {
            TempData["DeleteError"] = "Cannot delete bordspellenavond with subscribers";
            return RedirectToAction("Index");
        }

        // Check if the current user is the owner of the bordspellenavond
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // assuming you are using ASP.NET Identity
        if(bordspellenavond.OrganisatorId != userId)
        {
            TempData["DeleteError"] = "You can only delete your own bordspellenavond";
            return RedirectToAction("Index");
        }

        return View(bordspellenavond);
    }

// POST: Bordspellenavond/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(policy : "MinimumAge")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var bordspellenavond = await _repo.GetByIdAsync(id);
        if (bordspellenavond == null)
        {
            return NotFound();
        }

        // Check if there are any participants
        if(bordspellenavond.Deelnemers.Any())
        {
            TempData["DeleteError"] = "Cannot delete bordspellenavond with subscribers";
            return RedirectToAction("Index");
        }

        // Check if the current user is the owner of the bordspellenavond
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // assuming you are using ASP.NET Identity
        if(bordspellenavond.OrganisatorId != userId)
        {
            TempData["DeleteError"] = "You can only delete your own bordspellenavond";
            return RedirectToAction("Index");
        }

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

        // Check if the Bordspellenavond is full
        if (bordspellenavond.Deelnemers.Count >= bordspellenavond.MaxAantalSpelers)
        {
            ModelState.AddModelError("SubscriptionError", "De bordspellenavond is vol.");
            return View("Error"); // Replace with your error view
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
        
        // Check dieetwensen
        if(!gebruiker.Dieetwensen.HasFlag(bordspellenavond.Dieetwensen)) {
    
            TempData["DietWarning"] = "Let op: je dieetwensen komen niet overeen met wat er geserveerd wordt bij deze bordspellenavond.";

        }

        // Check allergieën
        if(bordspellenavond.Dieetwensen.HasFlag(Dieetwensen.Notenallergie) && 
           gebruiker.Dieetwensen.HasFlag(Dieetwensen.Notenallergie)) {
     
            TempData["AllergyWarning"] = "Let op: er worden noten geserveerd bij deze bordspellenavond.";

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
        [HttpGet("CreatePotluckItem/{bordspellenavondId:int}")]
        public IActionResult CreatePotluckItem(int bordspellenavondId)
        {
            ViewBag.BordspellenavondId = bordspellenavondId;
            return View();
        }
        
        [HttpPost("CreatePotluckItem/{bordspellenavondId:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePotluckItem(int bordspellenavondId, PotluckItem potluckItem)
        {
            if (ModelState.IsValid)
            {
                var bordspellenavond = await _repo.GetByIdAsync(bordspellenavondId);
                var user = await _userManager.GetUserAsync(User);
        
                if (bordspellenavond == null || user == null)
                {
                    return NotFound();
                }

                potluckItem.Bordspellenavond = bordspellenavond;
                potluckItem.Participant = user;

                bordspellenavond.PotluckItems.Add(potluckItem);
                await _repo.UpdateAsync(bordspellenavond);

                return RedirectToAction("Details", new { id = bordspellenavondId });
            }

            if (!ModelState.IsValid)
            {
                // ModelState is not valid, let's see why
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToArray();

                foreach (var error in errors)
                {
                    foreach (var subError in error.Errors)
                    {
                        Console.WriteLine($"Property: {error.Key} Error: {subError.ErrorMessage}");
                    }
                }
            }

            ViewBag.BordspellenavondId = bordspellenavondId;
            return View(potluckItem);
        }
        }
    