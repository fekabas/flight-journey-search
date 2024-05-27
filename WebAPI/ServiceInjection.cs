using BusinessLayer.ExternalServices.FlightAPIService;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataLayer;

namespace WebAPI;

/// <summary>
/// WebApp's service injection.
/// </summary>
internal class ServiceInjection : AbstractServiceInjection
{

    public ServiceInjection(IServiceCollection services, IConfiguration configuration)
        : base(services, configuration)
    {
    }

    public override IServiceCollection Initialize()
    {
        Services.AddHttpClient();

        AddServices();
        AddBusinessLogics();
        AddConfigurations();

        return Services;
    }
    private void AddServices()
    {
        Services.AddScoped<IFileService, FileService>();
        Services.AddScoped<IFlightAPIService, FlightAPIService>();
    }

    private void AddBusinessLogics()
    {
    }

    private void AddConfigurations()
    {
        Services.AddSingleton<IConfiguration>(x => new FlightJourneySearchConfiguration(x.GetRequiredService<IWebHostEnvironment>(), x.GetRequiredService<IServiceProvider>()));
        Services.AddSingleton<FlightAPIServiceConfiguration>();
    }
}