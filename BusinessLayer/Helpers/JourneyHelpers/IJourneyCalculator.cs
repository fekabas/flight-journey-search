using BusinessLayer.BusinessLogic.DTOs.FlightDTOs;
using BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;

public interface IJourneyCalculator
{
    List<JourneyRes> FindRoute(List<FlightItemRes> flights, string origin, string destination, uint maxLayovers = 1);
}