using BoardGameNight.Data;
using BoardGameNight.Models;
using BoardGameNight.Repositories;
using BoardGameNight.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            .ToListAsync();
    }

    public async Task<Bordspellenavond> GetByIdAsync(int id)
    {
        return await _context.Bordspellenavonden
            .Include(b => b.Bordspellen)
            .Include(b => b.Organisator)
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

}