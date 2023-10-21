using BoardGameNight.Models;

namespace BoardGameNight.Repositories.Interfaces
{
    public interface IBordspellenavondRepository
    {
        Task<List<Bordspellenavond>> GetAllAsync();
        Task<Bordspellenavond> GetByIdAsync(int id);

        Task CreateAsync(Bordspellenavond bordspellenavond, string userId);
        Task UpdateAsync(Bordspellenavond bordspellenavond);
        Task DeleteAsync(int id);
        Task AddBordspelAsync(int bordspellenavondId, Bordspel bordspel);
    }
}