using BoardGameNight.Models;

namespace BoardGameNight.Repositories
{
    public interface IEtenRepository
    {
        Task<List<Eten>> GetAllAsync();
        Task<Eten> GetByIdAsync(int id);
        Task CreateAsync(Eten eten);
        Task UpdateAsync(Eten eten);
        Task DeleteAsync(int id);
    }
}