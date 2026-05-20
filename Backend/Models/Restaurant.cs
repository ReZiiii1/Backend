using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("restauracje")]
public class Restauracja
{
    public int Nr_restauracji { get; init; }
    public string Miejscowosc { get; init; } = null!;
    public string Ulica { get; init; } = null!;
    public int Nr_budynku { get; init; } = 0!;
    public int? Nr_lokalu { get; init; }

    public decimal Dlugosc_geo { get; init; }

    public decimal Szerokosc_geo { get; init; }
}