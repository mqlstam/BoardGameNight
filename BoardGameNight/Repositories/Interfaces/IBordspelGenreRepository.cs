
namespace BoardGameNight.Repositories.Interfaces;

public interface IBordspelGenreRepository
{
    Task<List<BordspelGenre>> GetAllAsync();
    Task<BordspelGenre> GetByIdAsync(int id);
}