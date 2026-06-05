using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class ManticoreContext(DbContextOptions<ManticoreContext> options) : DbContext(options)
{
    public DbSet<Restauracja> Restauracje { get; set; } = null!;
    public DbSet<Produkt> Produkty { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Order> Orders { get; set; }=null!;
    public DbSet<OrderItem> OrderItems { get; set; }=null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restauracja>().ToTable("restauracje");
        modelBuilder.Entity<Restauracja>().HasKey(r => r.Nr_restauracji);

        modelBuilder.Entity<Produkt>().ToTable("produkty");

        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>().HasKey(u => u.Id);
    }
}
