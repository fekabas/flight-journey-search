using BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;
using BusinessLayer.ExternalServices.DTOs.FlightAPIServiceDTOs;
using BusinessLayer.Interfaces;
using Entities.Models;

namespace BusinessLayer.BusinessLogic;
public class JourneyBusinessLogic : IJourneyBusinessLogic
{
    private readonly IFlightAPIService flightAPIService;
    private readonly IJourneyCalculator journeyCalculator;
    public JourneyBusinessLogic(
        IFlightAPIService flightAPIService,
        IJourneyCalculator journeyCalculator
        )
    {
        this.flightAPIService = flightAPIService;
        this.journeyCalculator = journeyCalculator;
    }
    
    public async Task<List<JourneyRes>?> CalculateJourneyAsync(string origin, string destination)
    {
        ICollection<FlightItemRes> flights = await flightAPIService.GetFlightsAsync();

        var journeyFlights = journeyCalculator.FindRoute(flights.ToList(), origin, destination);

        if (journeyFlights is null || !journeyFlights.Any())
            return null;

        return journeyFlights;
    }
}