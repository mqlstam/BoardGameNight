namespace BoardGameNight.Models;

public class Review
{
    public int Id { get; set; }
    public int Score { get; set; }
    public string Tekst { get; set; }

    // Navigational properties
    public Persoon Persoon { get; set; }
    public string PersoonId { get; set; }
    public Bordspellenavond Bordspellenavond { get; set; }
    public int BordspellenavondId { get; set; }
}