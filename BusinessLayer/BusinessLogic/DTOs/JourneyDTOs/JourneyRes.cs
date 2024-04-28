using BusinessLayer.ExternalServices.DTOs.FlightAPIServiceDTOs;

namespace BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;

public class JourneyRes
{
    public JourneyRes()
    {
        
    }

    public List<FlightItemRes>? Flights { get; set; }
}