using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BoardGameNight.Models
{
    public class Bordspel 
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Naam is required.")]
        [StringLength(100, ErrorMessage = "Naam cannot be longer than 100 characters.")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Beschrijving is required.")]
        [StringLength(1000, ErrorMessage = "Beschrijving cannot be longer than 1000 characters.")]
        public string Beschrijving { get; set; }

        [Required(ErrorMessage = "Genre is required.")]
        [StringLength(100, ErrorMessage = "Genre cannot be longer than 100 characters.")]
        public string Genre { get; set; }

        public bool Is18Plus { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string? FotoUrl { get; set; }

        // Let's assume that SoortSpel can be null, but if it's provided, it shouldn't be longer than 100 characters
        [StringLength(100, ErrorMessage = "SoortSpel cannot be longer than 100 characters.")]
        public string? SoortSpel { get; set; }

        public ICollection<Bordspellenavond>? Bordspellenavonden { get; set; }
    }
}