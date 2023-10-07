namespace BoardGameNight.Models;

public class Bordspellenavond
{
    public int Id { get; set; }
    public string Adres { get; set; }
    public int MaxAantalSpelers { get; set; }
    public DateTime DatumTijd { get; set; }
    public bool Is18Plus { get; set; }

    // Navigational properties
    public Persoon Organisator { get; set; }
    public int OrganisatorId { get; set; }
    public ICollection<Persoon> Deelnemers { get; set; }
    public ICollection<Bordspel> Bordspellen { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Eten> Eten { get; set; }
}