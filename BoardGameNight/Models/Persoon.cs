namespace BoardGameNight.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class Persoon : IdentityUser
    {
        public string Naam { get; set; }
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
}