namespace BoardGameNight.Models;

public class Eten
{
    public int Id { get; set; }
    public string Naam { get; set; }
    public bool IsVegetarisch { get; set; }
    public bool IsLactosevrij { get; set; }
    public bool IsNotenvrij { get; set; }
    public bool IsAlcoholvrij { get; set; }

    // Navigational properties
    public Bordspellenavond Bordspellenavond { get; set; }
    public int BordspellenavondId { get; set; }
}