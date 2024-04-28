using BusinessLayer.BusinessLogic.DTOs.FlightDTOs;
using BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;
using BusinessLayer.BusinessLogic.DTOs.TransportDTOs;
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
    
    public async Task<List<FlightCombinationRes>?> GetCombinationsAsync(string origin, string destination, int maxLayovers = 1)
    {
        IEnumerable<FlightItemRes> flights = (await flightAPIService.GetFlightsAsync())
        .Select(f => new FlightItemRes()
        {
            Origin = f.DepartureStation,
            Destination = f.ArrivalStation,
            Price = f.Price,
            Transport = new TransportItemRes()
            {
                FlightCarrier = f.FlightCarrier,
                FlightNumber = f.FlightNumber
            }
        });

        var journeyFlights = journeyCalculator.FindRoute(flights.ToList(), origin, destination, maxLayovers);

        if (journeyFlights is null || !journeyFlights.Any())
            return null;

        return journeyFlights;
    }
}