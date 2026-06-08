using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromotionsController(PromotionService promotionService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBasic()
    {
        try
        {
            var promotions = await promotionService.GetBasicPromotionsAsync();
            var result = promotions.Select(p => MapPromotion(p, false));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Błąd podczas pobierania promocji.", error = ex.Message });
        }
    }

    [HttpGet("premium")]
    public async Task<IActionResult> GetPremium()
    {
        try
        {
            var isAuthenticated = User.Identity?.IsAuthenticated ?? false;
            var promotions = await promotionService.GetPremiumPromotionsAsync();
            var result = promotions.Select(p => MapPromotion(p, !isAuthenticated));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Błąd podczas pobierania promocji premium.", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PromotionDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest(new { message = "Brak danych promocji." });

            var promotion = await promotionService.CreatePromotionAsync(dto);

            return StatusCode(201, new
            {
                message = "Promocja została dodana.",
                data = MapPromotion(promotion, promotion.IsPremium)
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Błąd podczas dodawania promocji.", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] PromotionDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest(new { message = "Brak danych promocji." });

            var promotion = await promotionService.UpdatePromotionAsync(id, dto);

            return Ok(new
            {
                message = "Promocja została zaktualizowana.",
                data = MapPromotion(promotion, promotion.IsPremium)
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Błąd podczas edycji promocji.", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await promotionService.DeletePromotionAsync(id);

            return Ok(new { message = "Promocja została usunięta." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Błąd podczas usuwania promocji.", error = ex.Message });
        }
    }

    private static object MapPromotion(Promotion p, bool isLocked) => new
    {
        id = p.Id,
        nazwa = p.Nazwa,
        opis = p.Opis,
        parsedPrice = p.Cena,
        imageUrl = p.Zdjecie,
        isLocked,
        isPremium = p.IsPremium,
        itemType = "promotion"
    };
}
