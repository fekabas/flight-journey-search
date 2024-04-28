using BusinessLayer.BusinessLogic;
using BusinessLayer.BusinessLogic.Helpers.JourneyHelpers;
using BusinessLayer.Interfaces;
using BusinessLayer.JobScheduler.JobConfiguration;
using DataLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tools.Helpers.Configuration;
using Tools.Interfaces.Configuration;

namespace BusinessLayer;

/// <summary>
/// Business Layer's service injection.
/// </summary>
internal class ServiceInjection(IServiceCollection services, IConfiguration configuration) : AbstractServiceInjection(services, configuration)
{
    public override IServiceCollection Initialize()
    {
        Services.AddHttpClient();

        AddBusinessLogics();
        AddConfigurations();

        Services.AddTransient<IQuartzJobServiceInjection, QuartzJobServiceInjection>();

        return Services;
    }
    private void AddBusinessLogics()
    {
        Services.AddScoped<IReadingBusinessLogic, ReadingBusinessLogic>();
        Services.AddScoped<IJourneyBusinessLogic, JourneyBusinessLogic>();
        Services.AddSingleton<IJourneyCalculator, JourneyCalculator>();
    }
    private void AddConfigurations()
    {
        Services.AddSingleton<IConfiguration>(x => new FlightJourneySearchConfiguration(x.GetRequiredService<IWebHostEnvironment>(), x.GetRequiredService<IServiceProvider>()));

        Services.AddScoped<IGeneralConfiguration, GeneralConfiguration>();
        Services.AddScoped<IRealmConfiguration, RealmConfiguration>();
    }
}