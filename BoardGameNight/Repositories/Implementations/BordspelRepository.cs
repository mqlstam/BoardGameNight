using BoardGameNight.Data;
using BoardGameNight.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardGameNight.Repositories.Implementations;


public class BordspelRepository : IBordspelRepository
{
    
    private readonly ApplicationDbContext _context;

    public BordspelRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Bordspel>> GetAllAsync()
    {
        return await _context.Bordspellen.ToListAsync();
    }

    public async Task<Bordspel> GetByIdAsync(int id)
    {
        return await _context.Bordspellen.FindAsync(id);
    }

    public async Task CreateAsync(Bordspel bordspel)
    {
        _context.Bordspellen.Add(bordspel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Bordspel bordspel)
    {
        _context.Entry(bordspel).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var bordspel = await _context.Bordspellen.FindAsync(id);
        if (bordspel != null)
        {
            _context.Bordspellen.Remove(bordspel);
            await _context.SaveChangesAsync();
        }
    }
}