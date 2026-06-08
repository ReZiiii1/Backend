using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("promocje")]
public class Promotion
{
    public int Id { get; set; }
    public string Nazwa { get; set; } = string.Empty;
    public string Opis { get; set; } = string.Empty;
    public decimal Cena { get; set; }

    [Column("zdjęcie")]
    public string Zdjecie { get; set; } = string.Empty;

    public bool IsPremium { get; set; }
}
