using System.ComponentModel.DataAnnotations;
using BoardGameNight.Attributes;

namespace BoardGameNight.Models
{
    public class Bordspellenavond
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Adres is vereist.")]
        [StringLength(100, ErrorMessage = "Adres mag niet meer dan 100 karakters zijn.")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Maximaal aantal spelers is vereist.")]
        [Range(1, 100, ErrorMessage = "Maximaal aantal spelers moet tussen 1 en 100 liggen.")]
        public int MaxAantalSpelers { get; set; }

        [Required(ErrorMessage = "Datum en tijd zijn vereist.")]
        [FutureDate(ErrorMessage = "Datum en tijd moeten in de toekomst liggen.")]
        public DateTime DatumTijd { get; set; }

        public bool Is18Plus { get; set; }

        [Required(ErrorMessage = "Dieetwensen is vereist.")]
        public Dieetwensen Dieetwensen { get; set; }

        [Required(ErrorMessage = "Drankvoorkeur is vereist.")]
        public DrankVoorkeur DrankVoorkeur { get; set; }

        // Navigational properties
        public Persoon? Organisator { get; set; }

        public string? OrganisatorId { get; set; }

        public ICollection<Persoon> Deelnemers { get; set; } = new List<Persoon>();
        public ICollection<Bordspel> Bordspellen { get; set; } = new List<Bordspel>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}