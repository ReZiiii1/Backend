using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class MenuContext(DbContextOptions<MenuContext> options) : DbContext(options)
{
    public DbSet<Produkt> Produkty { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Produkt>().ToTable("produkty");
    }
}