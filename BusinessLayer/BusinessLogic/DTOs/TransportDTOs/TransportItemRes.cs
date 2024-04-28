using Entities.Models;

namespace BusinessLayer.BusinessLogic.DTOs.TransportDTOs;

public class TransportItemRes
{
    public TransportItemRes()
    {
    }
    public TransportItemRes(Transport transport)
    {
        Id = transport.Id;
        FlightCarrier = transport.FlightCarrier;
        FlightNumber = transport.FlightNumber;
    }
    public Guid Id { get; set; }
    public string FlightCarrier { get; set; }
    public string FlightNumber { get; set; }

    public Transport ToEntity() => new Transport
    {
        Id = Id,
        FlightCarrier = FlightCarrier,
        FlightNumber = FlightNumber
    };
}