using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("produkty")]

public class Produkt
{
    public int Id { get; init; }
    public string Kategoria { get; init; } = null!;
    public string Nazwa { get; init; } = null!;
    public string? Opis { get; init; }
    public decimal Cena { get; init; }
    [Column("zdjęcie")] 
    public string? Zdjecie { get; init; }
}
