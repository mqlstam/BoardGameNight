namespace BoardGameNight.Models;

public class PersoonBordspellenavond
{
    public int PersoonId { get; set; }
    public Persoon Persoon { get; set; }

    public int BordspellenavondId { get; set; }
    public Bordspellenavond Bordspellenavond { get; set; }
}