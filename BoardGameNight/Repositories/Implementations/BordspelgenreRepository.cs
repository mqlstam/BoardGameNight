using BoardGameNight.Data;
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
}
