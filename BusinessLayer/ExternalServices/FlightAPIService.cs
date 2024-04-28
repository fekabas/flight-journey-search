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
    private static string BaseURL => "https://run.mocky.io/v3/73437984-a7e6-46b2-ac53-92124baa3383";
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
