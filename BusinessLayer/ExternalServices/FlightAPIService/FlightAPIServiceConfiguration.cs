using Microsoft.Extensions.Configuration;

namespace BusinessLayer.ExternalServices.FlightAPIService;

public class FlightAPIServiceConfiguration
{
        public string? FetchUrl { get; set; }

        public FlightAPIServiceConfiguration() { }

        public FlightAPIServiceConfiguration(IConfiguration configuration)
        {
            this.Bind(configuration);
        }

        public FlightAPIServiceConfiguration Bind(IConfiguration Configuration)
        {
            Configuration.GetSection(nameof(FlightAPIServiceConfiguration)).Bind(this);
            return this;
        }
}