using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class RestaurantContext(DbContextOptions<RestaurantContext> options) : DbContext(options)
{
    public DbSet<Restauracja> Restauracje { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Restauracja>().ToTable("restauracje");
        modelBuilder.Entity<Restauracja>().HasKey(r => r.Nr_restauracji);
    }
}