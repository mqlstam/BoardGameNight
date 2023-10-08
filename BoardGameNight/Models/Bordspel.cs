namespace BoardGameNight.Models;

public class Bordspel 
{
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public string Genre { get; set; }
    public bool Is18Plus { get; set; }
    public string? FotoUrl { get; set; }
    public string? SoortSpel { get; set; } // SoortSpel is already optional as it's a string
    // Navigational properties
    public ICollection<Bordspellenavond>? Bordspellenavonden { get; set; } // Now Bordspellenavonden is optional
}