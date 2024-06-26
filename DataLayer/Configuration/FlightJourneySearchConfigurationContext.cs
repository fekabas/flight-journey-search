using DataLayer.Constants;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Configuration;

internal class FlightJourneySearchConfigurationContext(string connectionString, string modelProvider) : DbContext
{
    private readonly string connectionString = connectionString;
    private readonly string modelProvider = modelProvider;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        switch (modelProvider)
        {
            case DatabaseProviders.SqlServer:
                optionsBuilder.UseSqlServer(connectionString);
                break;
            case DatabaseProviders.PostgreSQL:
                optionsBuilder.UseNpgsql(connectionString);
                break;
            default:
                throw new NotImplementedException($"The database provider '{modelProvider}' specified for the Model is not supported. ");
        }
    }
}