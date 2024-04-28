using System.Collections.ObjectModel;
using System.Net;
using BusinessLayer.ExternalServices.DTOs.FlightAPIServiceDTOs;
using BusinessLayer.Interfaces;
using Newtonsoft.Json;

namespace BusinessLayer.ExternalServices;

public class FlightAPIService : IFlightAPIService
{
    #region Properties
    private readonly IHttpClientFactory clientFactory;
    private static string BaseURL => "https://run.mocky.io/v3/11e9d99d-6c6a-4915-8038-7eeea1b35939";
    #endregion

    #region Constructor
    public FlightAPIService(
        IHttpClientFactory clientFactory
        )
    {
        this.clientFactory = clientFactory;
    }
    #endregion

    #region Public Methods
    public async Task<ICollection<FlightItemRes>> GetFlightsAsync()
    {
        using (var client = this.clientFactory.CreateClient())
        {
            HttpResponseMessage response = await client.GetAsync(BaseURL);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception("Exception Message");

                throw new Exception(response.Content.ToString());
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            if (responseContent is not null)
                return JsonConvert.DeserializeObject<ICollection<FlightItemRes>>(responseContent) ?? new Collection<FlightItemRes>();
            else
                return new Collection<FlightItemRes>();
        }
    }
    #endregion
}
