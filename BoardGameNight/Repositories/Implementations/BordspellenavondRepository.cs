using BoardGameNight.Data;
using BoardGameNight.Models;
using BoardGameNight.Repositories;
using BoardGameNight.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class BordspellenavondRepository : IBordspellenavondRepository
{
    private readonly ApplicationDbContext _context;

    public BordspellenavondRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Bordspellenavond>> GetAllAsync()
    {
        return await _context.Bordspellenavonden
            .Include(b => b.Bordspellen)
            .Include(b => b.Organisator)
            .Include(b => b.Deelnemers)
            .Include(b => b.PotluckItems)
            .ToListAsync();
    }

    public async Task<Bordspellenavond> GetByIdAsync(int id)
    {
        return await _context.Bordspellenavonden
            .Include(b => b.Bordspellen)
            .Include(b => b.Organisator)
            .Include(b => b.Deelnemers)
            .Include(b => b.PotluckItems)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task CreateAsync(Bordspellenavond bordspellenavond, string userId)
    {
        bordspellenavond.Organisator.Id = userId;
        _context.Bordspellenavonden.Add(bordspellenavond);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Bordspellenavond bordspellenavond)
    {
        _context.Entry(bordspellenavond).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var bordspellenavond = await _context.Bordspellenavonden.FindAsync(id);
        if (bordspellenavond != null)
        {
            _context.Bordspellenavonden.Remove(bordspellenavond);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddBordspelAsync(int bordspellenavondId, Bordspel bordspel)
    {
        var bordspellenavond = await GetByIdAsync(bordspellenavondId);
        if (bordspellenavond != null)
        {
            bordspellenavond.Bordspellen.Add(bordspel);
            await UpdateAsync(bordspellenavond);
        }
    }
    
    public async Task<bool> CanUserJoinGameNight(string userId, DateTime gameNightDate)
    {
        var userGameNights = await _context.Bordspellenavonden
            .Include(b => b.Deelnemers)
            .Where(b => b.DatumTijd.Date == gameNightDate.Date)
            .ToListAsync();

        return !userGameNights.Any(g => g.Deelnemers.Any(d => d.Id == userId));
    }

    public async Task JoinGameNight(int gameNightId, string userId, DateTime gameNightDate)
    {
        var canJoin = await CanUserJoinGameNight(userId, gameNightDate);
        if (!canJoin)
        {
            throw new Exception("Je kunt je niet aanmelden voor meer dan een spelavond per dag.");
        }

        var gameNight = await GetByIdAsync(gameNightId);
        if (gameNight != null)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                gameNight.Deelnemers.Add(user);
                await UpdateAsync(gameNight);
            }
            else
            {
                throw new Exception("Gebruiker niet gevonden.");
            }
        }
    }

    public async Task AddPotluckItemAsync(PotluckItem item)
    {
        // Get Bordspellenavond
        var bordspellenavond = await GetByIdAsync(item.BordspellenavondId); 

        // Add PotluckItem
        bordspellenavond.PotluckItems.Add(item);

        await UpdateAsync(bordspellenavond);
    }
}