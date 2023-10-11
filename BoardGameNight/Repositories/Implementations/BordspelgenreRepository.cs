using BoardGameNight.Data;
using BoardGameNight.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BoardGameNight.Repositories.Implementations;
public class BordspelGenreRepository : IBordspelGenreRepository
{
    private readonly ApplicationDbContext _context;

    public BordspelGenreRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<BordspelGenre>> GetAllAsync()
    {
        return await _context.BordspelGenres.ToListAsync();
    }
    
    public async Task<BordspelGenre> GetByIdAsync(int id)
    {
        return await _context.BordspelGenres.FirstOrDefaultAsync(m => m.Id == id);
    }
}
