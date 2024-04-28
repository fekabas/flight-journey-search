using BusinessLayer.BusinessLogic.DTOs.FlightDTOs;
using BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;
using BusinessLayer.BusinessLogic.DTOs.TransportDTOs;
using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.BusinessLogic;
public class JourneyBusinessLogic : IJourneyBusinessLogic
{
    private readonly IFlightAPIService flightAPIService;
    private readonly IJourneyCalculator journeyCalculator;
    private readonly IService<Journey> journeyService;
    public JourneyBusinessLogic(
        IFlightAPIService flightAPIService,
        IJourneyCalculator journeyCalculator,
        IService<Journey> journeyService
        )
    {
        this.flightAPIService = flightAPIService;
        this.journeyCalculator = journeyCalculator;
        this.journeyService = journeyService;
    }
    
    public async Task<List<JourneyRes>?> GetCombinationsAsync(string origin, string destination, uint numberOfFlighs = 1)
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

        var journeyFlights = journeyCalculator.FindRoute(flights.ToList(), origin, destination, numberOfFlighs);

        if (journeyFlights is null || !journeyFlights.Any())
            return null;

        return journeyFlights;
    }

    public async Task<JourneyRes?> GetCheapestFlightAsync(string origin, string destination, uint numberOfFlighs = 1)
    {
        Journey? dbJourney = await journeyService.GetAll()
                            .Include(j => j.Flights)
                                .ThenInclude(jf => jf.Flight)
                                .ThenInclude(f => f!.Transport)
                            .Where(j =>
                                j.Origin == origin
                                && j.Destination == destination
                                && j.Flights.Count() < numberOfFlighs + 1)
                            .OrderBy(j => j.Flights.Sum(f => f.Flight!.Price))
                            .FirstOrDefaultAsync();

        if(dbJourney is not null)
            return new JourneyRes(dbJourney);

        // Try to calculate the route
        var journeyCombinations = await GetCombinationsAsync(origin, destination, numberOfFlighs);
        var cheapestJourney = journeyCombinations?.OrderBy(j => j.Flights!.Sum(f => f.Price)).FirstOrDefault();
        
        if(cheapestJourney is null)
            return null;
        else
        {
            Journey journey = cheapestJourney.ToEntity();
            await journeyService.AddAsync(journey);
            return new JourneyRes(journey);
        }
    }
}