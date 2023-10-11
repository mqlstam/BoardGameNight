using BoardGameNight.Models;

namespace BoardGameNight.Repositories.Interfaces;

public interface IBordspelRepository
{
    Task<List<Bordspel>> GetAllAsync();
    Task<Bordspel> GetByIdAsync(int id);
    Task CreateAsync(Bordspel bordspel);
    Task UpdateAsync(Bordspel bordspel);
    Task DeleteAsync(int id);
    
}