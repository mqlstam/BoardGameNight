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

        Task<bool> CanUserJoinGameNight(string userId, DateTime gameNightDate);

        Task JoinGameNight(int gameNightId, string userId, DateTime gameNightDate);
        Task AddPotluckItemAsync(PotluckItem item);
        
        Task<List<Bordspellenavond>> GetUserSubscriptions(string userId);

    }
}