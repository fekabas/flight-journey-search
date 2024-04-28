using DataLayer.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DataLayer.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddFlightJourneySearchConfiguration(this IConfigurationBuilder builder, IHostEnvironment env)
        {
            IConfigurationRoot configuration = builder
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .Build();

            DatabaseConfiguration db = new DatabaseConfiguration().Bind(configuration);

            return builder.Add(new FlightJourneySearchConfigurationSource(db.ModelConnection ?? "defaultConnection", db.ModelProvider ?? "defaultProvider"));
        }
    }
}