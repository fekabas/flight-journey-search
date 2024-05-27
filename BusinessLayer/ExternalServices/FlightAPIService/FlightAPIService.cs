using System.Collections.ObjectModel;
using System.Net;
using BusinessLayer.ExternalServices.FlightAPIService.DTOs;
using BusinessLayer.Interfaces;
using Newtonsoft.Json;

namespace BusinessLayer.ExternalServices.FlightAPIService;

public class FlightAPIService : IFlightAPIService
{
    #region Properties
    private readonly IHttpClientFactory clientFactory;
    private readonly FlightAPIServiceConfiguration flightAPIServiceConfiguration;
    #endregion

    #region Constructor
    public FlightAPIService(
        FlightAPIServiceConfiguration flightAPIServiceConfiguration,
        IHttpClientFactory clientFactory
        )
    {
        this.clientFactory = clientFactory;
        this.flightAPIServiceConfiguration = flightAPIServiceConfiguration;
    }
    #endregion

    #region Public Methods
    public async Task<ICollection<FlightAPIItemRes>> GetFlightsAsync()
    {
        using (var client = this.clientFactory.CreateClient())
        {
            HttpResponseMessage response = await client.GetAsync(flightAPIServiceConfiguration.FetchUrl);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception("Exception Message");

                throw new Exception(response.Content.ToString());
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            if (responseContent is not null)
                return JsonConvert.DeserializeObject<ICollection<FlightAPIItemRes>>(responseContent) ?? new Collection<FlightAPIItemRes>();
            else
                return new Collection<FlightAPIItemRes>();
        }
    }
    #endregion
}
