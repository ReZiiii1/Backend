namespace Backend.Models;

public class PromotionDto
{
    public string Nazwa { get; set; } = string.Empty;
    public string Opis { get; set; } = string.Empty;
    public decimal Cena { get; set; }
    public string Zdjecie { get; set; } = string.Empty;
    public bool IsPremium { get; set; }
}
