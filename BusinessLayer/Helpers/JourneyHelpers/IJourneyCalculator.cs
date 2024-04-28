using BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;
using BusinessLayer.ExternalServices.DTOs.FlightAPIServiceDTOs;

public interface IJourneyCalculator
{
    List<JourneyRes> FindRoute(List<FlightItemRes> flights, string origin, string destination, int maxConnections = 2);
}