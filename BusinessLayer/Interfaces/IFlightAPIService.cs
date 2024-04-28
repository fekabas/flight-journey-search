using BusinessLayer.ExternalServices.DTOs.FlightAPIServiceDTOs;

namespace BusinessLayer.Interfaces
{
    public interface IFlightAPIService
    {
        /// <summary>
        /// Get available flights.
        /// </summary>
        Task<ICollection<FlightItemRes>> GetFlightsAsync();
    }
}
