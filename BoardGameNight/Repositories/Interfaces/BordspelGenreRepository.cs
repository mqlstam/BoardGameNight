namespace BoardGameNight.Repositories;
public interface IBordspelGenreRepository
{
    Task<List<BordspelGenre>> GetAllAsync();
}