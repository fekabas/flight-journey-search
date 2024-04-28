using BusinessLayer.BusinessLogic.DTOs.TransportDTOs;
using Entities.Models;

namespace BusinessLayer.BusinessLogic.DTOs.FlightDTOs;

public class FlightItemRes
{
    public FlightItemRes()
    {
    }
    public FlightItemRes(Flight flight)
    {
        Id = flight.Id;
        Origin = flight.Origin;
        Destination = flight.Destination;
        Price = flight.Price;
        Transport = new TransportItemRes(flight.Transport!);
    }
    public Guid Id { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public double Price { get; set; }
    public TransportItemRes Transport { get; set; }

    public Flight ToEntity() => new Flight
    {
        Id = Id,
        Origin = Origin,
        Destination = Destination,
        Price = Price,
        Transport = Transport.ToEntity()
    };
}