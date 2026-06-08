using Backend.Data;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    ManticoreContext context,
    JwtService jwtService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { message = "E-mail i hasło są wymagane." });

            if (await context.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest(new { message = "Użytkownik o tym adresie e-mail już istnieje!" });

            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return Ok(new { message = "Rejestracja pomyślna!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Błąd podczas rejestracji.", error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { message = "E-mail i hasło są wymagane." });

            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return BadRequest(new { message = "Nie znaleziono użytkownika o takim adresie e-mail." });

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return BadRequest(new { message = "Błędne hasło!" });

            var token = jwtService.GenerateToken(user);

            return Ok(new
            {
                message = "Logowanie pomyślne!",
                username = user.Username,
                email = user.Email,
                token
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Błąd podczas logowania.", error = ex.Message });
        }
    }
}
