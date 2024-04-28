using BusinessLayer.BusinessLogic.DTOs.TransportDTOs;

namespace BusinessLayer.BusinessLogic.DTOs.FlightDTOs;

public class FlightItemRes
{
    public FlightItemRes()
    {
    }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public double Price { get; set; }
    public TransportItemRes Transport { get; set; }
}