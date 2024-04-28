using Microsoft.Extensions.Configuration;

namespace DataLayer.Configuration;

public class FlightJourneySearchConfigurationSource(string connectionString, string modelProvider) : IConfigurationSource
{
    private readonly string _connectionString = connectionString;
    private readonly string _modelProvider = modelProvider;

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new FlightJourneySearchConfigurationProvider(_connectionString, _modelProvider);
    }
}