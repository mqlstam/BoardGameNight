using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BoardGameNight.Models
{
    public class PotluckItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Item name is required.")]
        [StringLength(100, ErrorMessage = "Item name can not be more than 100 characters.")]
        public string Name { get; set; }

        public Persoon? Participant { get; set; }

        public Dieetwensen? Dieetwensen { get; set; }

        // Navigational property
        public Bordspellenavond? Bordspellenavond { get; set; }
        public int BordspellenavondId { get; set; }
    }
}