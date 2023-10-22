using System.ComponentModel.DataAnnotations;

namespace BoardGameNight.Models;

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
    public int GenreId { get; set; } // foreign key

    [Required(ErrorMessage = "Soort spel is required.")]
    public int SoortSpelId { get; set; } // foreign key
    
    
    // Navigation property
    public BordspelGenre? Genre { get; set; }

    public SoortBordspel? SoortSpel { get; set; }
    
    [Required]
    public bool Is18Plus { get; set; }

    [Url(ErrorMessage = "Invalid URL format.")]
    public string? FotoUrl { get; set; }

    public ICollection<Bordspellenavond>? Bordspellenavonden { get; set; }
}