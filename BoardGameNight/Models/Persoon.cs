namespace BoardGameNight.Models;

public class Persoon 
{
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Email { get; set; }
    public char Geslacht { get; set; }
    public string Adres { get; set; }
    public DateTime Geboortedatum { get; set; }
    public string Dieetwensen { get; set; }
    public string Allergieën { get; set; }

    // Navigational properties
    public ICollection<Bordspellenavond> GeorganiseerdeAvonden { get; set; }
    public ICollection<Bordspellenavond> DeelgenomenAvonden { get; set; }
    public ICollection<Review> Reviews { get; set; }
    
}