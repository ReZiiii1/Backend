using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class ManticoreContext(DbContextOptions<ManticoreContext> options) : DbContext(options)
{
    // Wszystkie trzy tabele w jednym miejscu!
    public DbSet<Restauracja> Restauracje { get; set; } = null!;
    public DbSet<Produkt> Produkty { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mapowanie tabeli Restauracje
        modelBuilder.Entity<Restauracja>().ToTable("restauracje");
        modelBuilder.Entity<Restauracja>().HasKey(r => r.Nr_restauracji);

        // Mapowanie tabeli Produkty
        modelBuilder.Entity<Produkt>().ToTable("produkty");

        // Mapowanie tabeli Users (Użytkownicy)
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>().HasKey(u => u.Id);
    }
}
