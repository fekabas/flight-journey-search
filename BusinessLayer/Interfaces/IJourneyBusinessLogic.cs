using BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;

namespace BusinessLayer.Interfaces;

public interface IJourneyBusinessLogic
{
    /// <summary>
    /// Try to calculate all possible flight combinations given the origin and destination.
    /// </summary>
    Task<List<JourneyRes>?> GetCombinationsAsync(string origin, string destination, int numberOfFlighs = 1);

    /// <summary>
    /// Try to get the cheapest journey
    /// </summary>
    Task<JourneyRes?> GetCheapestFlightAsync(string origin, string destination, int numberOfFlighs = 1);
}