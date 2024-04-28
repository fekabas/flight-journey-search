using BusinessLayer.BusinessLogic.DTOs.FlightDTOs;
using Entities.Models;

namespace BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;

public class JourneyRes
{
    public JourneyRes()
    {
        
    }
    public JourneyRes(Journey journey)
    {
        Id = journey.Id;
        Origin = journey.Origin;
        Destination = journey.Destination;
        Price = journey.Price;
        Flights = journey.Flights.Select(f => new FlightItemRes(f.Flight!)).ToList();
    }
    public Guid Id { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public double Price { get; set; }

    public List<FlightItemRes>? Flights { get; set; }

    public Journey ToEntity() => new Journey
    {
        Id = Id,
        Origin = Origin,
        Destination = Destination,
        Price = Price,
        Flights = Flights!.Select(f => new FlightJourney()
        {
            Flight = f.ToEntity(),
            FlightId = f.Id,
            JourneyId = Id,
        })
        .ToList()
    };
}