using BoardGameNight.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoardGameNight.Repositories
{
    public interface IBordspellenavondRepository
    {
        Task<List<Bordspellenavond>> GetAllAsync();
        Task<Bordspellenavond> GetByIdAsync(int id);

        Task CreateAsync(Bordspellenavond bordspellenavond, string userId);
        Task UpdateAsync(Bordspellenavond bordspellenavond);
        Task DeleteAsync(int id);
        Task AddBordspelAsync(int bordspellenavondId, Bordspel bordspel);
        Task AddEtenAsync(int bordspellenavondId, Eten eten);
    }
}