namespace BoardGameNight.Repositories;

public interface ISoortBordspelRepository
{
    Task<List<SoortBordspel>> GetAllAsync();

}