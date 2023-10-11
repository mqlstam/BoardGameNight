using BoardGameNight.Data;
using BoardGameNight.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BoardGameNight.Repositories.Implementations;

public class SoortBordspelRepository : ISoortBordspelRepository
{
    private readonly ApplicationDbContext _context;

    public SoortBordspelRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SoortBordspel>> GetAllAsync()
    {
        return await _context.SoortBordspellen.ToListAsync();
    }
    
    public async Task<SoortBordspel?> GetByIdAsync(int id)
    {
        return await _context.SoortBordspellen.FirstOrDefaultAsync(m => m.Id == id);
    }
}