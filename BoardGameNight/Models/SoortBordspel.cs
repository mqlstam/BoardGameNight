using System.ComponentModel.DataAnnotations;
using BoardGameNight.Models;

public class SoortBordspel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Naam { get; set; }

    public ICollection<Bordspel> Bordspellen { get; set; }
}