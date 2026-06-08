using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class PromotionService(ManticoreContext context)
{
    public async Task<IReadOnlyList<Promotion>> GetBasicPromotionsAsync(
        CancellationToken cancellationToken = default)
    {
        return await context.Promocje
            .AsNoTracking()
            .Where(p => !p.IsPremium)
            .OrderBy(p => p.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Promotion>> GetPremiumPromotionsAsync(
        CancellationToken cancellationToken = default)
    {
        return await context.Promocje
            .AsNoTracking()
            .Where(p => p.IsPremium)
            .OrderBy(p => p.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Promotion> CreatePromotionAsync(
        PromotionDto dto,
        CancellationToken cancellationToken = default)
    {
        ValidateDto(dto);

        var promotion = new Promotion
        {
            Nazwa = dto.Nazwa.Trim(),
            Opis = dto.Opis.Trim(),
            Cena = dto.Cena,
            Zdjecie = dto.Zdjecie.Trim(),
            IsPremium = dto.IsPremium
        };

        context.Promocje.Add(promotion);
        await context.SaveChangesAsync(cancellationToken);

        return promotion;
    }

    public async Task<Promotion> UpdatePromotionAsync(
        int id,
        PromotionDto dto,
        CancellationToken cancellationToken = default)
    {
        ValidateDto(dto);

        var promotion = await context.Promocje.FindAsync([id], cancellationToken)
            ?? throw new KeyNotFoundException($"Promocja o ID {id} nie istnieje.");

        promotion.Nazwa = dto.Nazwa.Trim();
        promotion.Opis = dto.Opis.Trim();
        promotion.Cena = dto.Cena;
        promotion.Zdjecie = dto.Zdjecie.Trim();
        promotion.IsPremium = dto.IsPremium;

        await context.SaveChangesAsync(cancellationToken);

        return promotion;
    }

    public async Task DeletePromotionAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var promotion = await context.Promocje.FindAsync([id], cancellationToken)
            ?? throw new KeyNotFoundException($"Promocja o ID {id} nie istnieje.");

        context.Promocje.Remove(promotion);
        await context.SaveChangesAsync(cancellationToken);
    }

    private static void ValidateDto(PromotionDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nazwa))
            throw new ArgumentException("Nazwa promocji jest wymagana.");

        if (dto.Cena < 0)
            throw new ArgumentException("Cena nie może być ujemna.");
    }
}
