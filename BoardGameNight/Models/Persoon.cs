namespace BoardGameNight.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    public enum Geslacht
    {
        M, // Man
        V, // Vrouw
        X  // Niet gespecificeerd
    }
    [Flags]
    public enum Dieetwensen
    {
        Geen = 0,
        Vegetarisch = 1,
        Lactosevrij = 2,
        Notenallergie = 4
    }

    [Flags]
    public enum DrankVoorkeur
    {
        GeenVoorkeur = 0,
        Alcoholvrij = 1
    }

    public class Persoon : IdentityUser
    {
        public string Naam { get; set; }
        public Geslacht Geslacht { get; set; }
        public string Adres { get; set; }
        public DateTime Geboortedatum { get; set; }
        public Dieetwensen Dieetwensen { get; set; }

        public DrankVoorkeur DrankVoorkeur { get; set; }

        // Navigational properties
        public ICollection<Bordspellenavond> GeorganiseerdeAvonden { get; set; }
        public ICollection<Bordspellenavond> DeelgenomenAvonden { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public int GetAge()
        {
            var today = DateTime.Today;
            var age = today.Year - Geboortedatum.Year;
            if (Geboortedatum.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
    
}