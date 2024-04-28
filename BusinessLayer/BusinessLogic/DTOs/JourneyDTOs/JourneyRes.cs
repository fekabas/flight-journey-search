using BusinessLayer.BusinessLogic.DTOs.FlightDTOs;

namespace BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;

public class JourneyRes
{
    public JourneyRes()
    {
        
    }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public double Price { get; set; }

    public List<FlightItemRes>? Flights { get; set; }
}