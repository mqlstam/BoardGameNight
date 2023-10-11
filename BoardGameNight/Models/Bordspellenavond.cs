namespace BoardGameNight.Models
{
    public class Bordspellenavond
    {
        public int Id { get; set; }
        public string Adres { get; set; }
        public int MaxAantalSpelers { get; set; }
        public DateTime DatumTijd { get; set; }
        public bool Is18Plus { get; set; }

        // Navigational properties
        public Persoon Organisator { get; set; }
        public string OrganisatorId { get; set; } // Changed from int to string
        public ICollection<Persoon> Deelnemers { get; set; } = new List<Persoon>();
        public ICollection<Bordspel> Bordspellen { get; set; } = new List<Bordspel>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Eten> Eten { get; set; } = new List<Eten>();
    }
}