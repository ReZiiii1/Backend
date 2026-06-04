using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data; // Ścieżka do Twojego folderu Data
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Tutaj wpisujemy MenuContext zamiast AppDbContext!
        private readonly ManticoreContext _context;
        public AuthController(ManticoreContext context) { _context = context; }

        // 1. REJESTRACJA: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            // Sprawdzamy, czy użytkownik o takim mailu już istnieje
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest("Użytkownik o tym adresie e-mail już istnieje!");
            }

            // SZYFROWANIE HASŁA za pomocą BCrypt
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Tworzymy nowego użytkownika do zapisu
            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(); // Zapis do bazy MySQL!

            return Ok(new { message = "Rejestracja pomyślna!" });
        }

        // 2. LOGOWANIE: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            // Szukamy użytkownika po mailu
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return BadRequest("Nie znaleziono użytkownika o takim adresie e-mail.");
            }

            // WERYFIKACJA HASŁA: BCrypt sam wie, jak porównać czyste hasło z haszem z bazy
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Błędne hasło!");
            }

            // Jeśli wszystko jest OK, zwracamy sukces i dane użytkownika
            return Ok(new
            {
                message = "Logowanie pomyślne!",
                username = user.Username,
                email = user.Email
            });
        }
    }
    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}