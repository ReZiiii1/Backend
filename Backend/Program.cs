using System.Text;
using Backend.Data;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySql")
    ?? throw new InvalidOperationException("Brak połączenia z bazą MySql.");

builder.Services.AddDbContext<ManticoreContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Brak klucza JWT w konfiguracji.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<RestaurantService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<PromotionService>();
builder.Services.AddSingleton<JwtService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ManticoreContext>();

    await db.Database.MigrateAsync();

    // Naprawa: pusta migracja mogła zostać zapisana w historii bez utworzenia tabeli
    var tableExists = await db.Database.SqlQueryRaw<int>(
        "SELECT COUNT(*) AS Value FROM information_schema.tables " +
        "WHERE table_schema = DATABASE() AND table_name = 'promocje'")
        .FirstOrDefaultAsync();

    if (tableExists == 0)
    {
        await db.Database.ExecuteSqlRawAsync(
            "DELETE FROM __EFMigrationsHistory WHERE MigrationId = '20260606184730_AddPromotionsTable'");
        await db.Database.MigrateAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
