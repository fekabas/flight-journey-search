using Entities.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models.DbContexts;

public class ModelsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ModelsDbContext(DbContextOptions<ModelsDbContext> options) : base(options)
    {
    }

    protected ModelsDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ApplyCurrentLayerEntityConfigurations(modelBuilder);
        GenerateSeedData(modelBuilder);
    }

    /// <summary>
    /// Apply entity type configurations for Entities' entities.
    /// </summary>
    private static void ApplyCurrentLayerEntityConfigurations(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Journey>().HasQueryFilter(e => !e.Deleted);
        modelBuilder.Entity<Journey>().HasIndex(e => new { e.Origin, e.Destination, e.Deleted});

        modelBuilder.Entity<Transport>().HasQueryFilter(e => !e.Deleted);
        modelBuilder.Entity<Transport>().HasIndex(e => e.Deleted);

        modelBuilder.Entity<Flight>().HasQueryFilter(e => !e.Deleted);
        modelBuilder.Entity<Flight>().HasIndex(e => e.Deleted);

        modelBuilder.Entity<FlightJourney>().HasKey(e => new { e.JourneyId, e.FlightId });
    }

    private void GenerateSeedData(ModelBuilder modelBuilder)
    {
        SeedManager seedManager = new(modelBuilder, Database);
        seedManager.ExecuteSeed();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}