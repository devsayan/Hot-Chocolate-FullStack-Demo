using Microsoft.EntityFrameworkCore;

public class PokemonContext : DbContext
{
    public DbSet<Pokemon> Pokemon { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=pokemon.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pokemon>()
            .HasIndex(p => new { p.id })
            .IsUnique();
    }
}