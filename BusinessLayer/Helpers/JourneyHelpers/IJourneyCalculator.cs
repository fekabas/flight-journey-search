using BusinessLayer.BusinessLogic.DTOs.FlightDTOs;
using BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;

public interface IJourneyCalculator
{
    List<FlightCombinationRes> FindRoute(List<FlightItemRes> flights, string origin, string destination, int maxConnections = 2);
}