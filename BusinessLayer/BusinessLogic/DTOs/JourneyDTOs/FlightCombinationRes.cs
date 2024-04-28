using BusinessLayer.BusinessLogic.DTOs.FlightDTOs;

namespace BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;

public class FlightCombinationRes
{
    public FlightCombinationRes()
    {
        
    }

    public List<FlightItemRes>? Flights { get; set; }
}