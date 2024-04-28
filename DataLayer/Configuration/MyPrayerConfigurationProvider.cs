using Microsoft.Extensions.Configuration;

namespace DataLayer.Configuration;

public class FlightJourneySearchConfigurationProvider : ConfigurationProvider
{
    private readonly string _connectionString;
    private readonly string _modelProvider;

    public FlightJourneySearchConfigurationProvider(string connectionString, string modelProvider)
    {
        _connectionString = connectionString;
        _modelProvider = modelProvider;
    }

    public override void Load()
    {
        using FlightJourneySearchConfigurationContext dbContext = new(_connectionString, _modelProvider);

        base.Load();
    }
}