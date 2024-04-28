using BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;

namespace BusinessLayer.Interfaces;

public interface IJourneyBusinessLogic
{
    /// <summary>
    /// Try to calculate a journey given the origin and destination provided.
    /// </summary>
    Task<List<FlightCombinationRes>?> GetCombinationsAsync(string origin, string destination, int layovers = 1);
}