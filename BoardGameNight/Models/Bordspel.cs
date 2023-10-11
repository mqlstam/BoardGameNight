using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BoardGameNight.Models
{
    // public enum BordspelGenres
    // {
    //     Strategie,
    //     Familie,
    //     Avontuur,
    //     Coöperatief,
    //     PartijOfSocialeSpellen,
    //     Oorlogsspellen,
    //     Fantasie,
    //     EconomischeSpellen,
    //     AbstracteSpellen,
    //     HistorischeSpellen,
    //     DeductieSpellen,
    //     HorrorSpellen,
    //     EducatieveSpellen,
    //     RollenspelSpellen,
    //     PuzzelSpellen
    // }
    //
    // public enum SoortBordspel
    // {
    //     Tegellegspellen,
    //     Kaartspellen,
    //     Dobbelsteenspellen,
    //     Miniatuurspellen,
    //     RollAndMoveSpellen,
    //     DeckBuildingSpellen,
    //     WorkerPlacementSpellen,
    //     CoOpSpellen,
    //     RollenspelRPGSpellen,
    //     SocialDeductionSpellen
    // }
    
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
        public BordspelGenre Genre { get; set; }

        public SoortBordspel? SoortSpel { get; set; }

        public bool Is18Plus { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string? FotoUrl { get; set; }


        public ICollection<Bordspellenavond>? Bordspellenavonden { get; set; }
    }
}