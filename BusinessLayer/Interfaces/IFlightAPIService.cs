using BusinessLayer.ExternalServices.FlightAPIService.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IFlightAPIService
    {
        /// <summary>
        /// Get available flights.
        /// </summary>
        Task<ICollection<FlightAPIItemRes>> GetFlightsAsync();
    }
}
