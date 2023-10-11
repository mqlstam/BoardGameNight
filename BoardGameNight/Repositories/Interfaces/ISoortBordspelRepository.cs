namespace BoardGameNight.Repositories.Implementations;


public interface ISoortBordspelRepository
{
    Task<List<SoortBordspel>> GetAllAsync();
    Task<SoortBordspel?> GetByIdAsync(int id);
    
}