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
    public enum Dieetwensen
    {
        Geen,
        Vegetarisch,
        Lactosevrij,
        Notenallergie
    }

    public enum DrankVoorkeur
    {
        GeenVoorkeur,
        Alcoholvrij
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
    }
}