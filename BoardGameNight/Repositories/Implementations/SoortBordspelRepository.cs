using BoardGameNight.Data;
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
}