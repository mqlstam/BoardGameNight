using System.ComponentModel.DataAnnotations;
using BoardGameNight.Models;

public class BordspelGenre
{
    public int Id { get; set; }

    [Microsoft.Build.Framework.Required]
    [StringLength(100)]
    public string Naam { get; set; }

    public ICollection<Bordspel> Bordspellen { get; set; }
}