using BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;
using BusinessLayer.ExternalServices.DTOs.FlightAPIServiceDTOs;

namespace BusinessLayer.BusinessLogic.Helpers.JourneyHelpers;

/// <summary>
/// Journey Calculator implementation that returns the first found flight combination
/// </summary>
public class JourneyCalculator : IJourneyCalculator
{
    public JourneyCalculator()
    {
        
    }
    public List<JourneyRes> FindRoute(List<FlightItemRes> flights, string origin, string destination, int maxConnections = 1)
    {
        // Dictionary to store explored paths (origin -> [connected flights])
        var explored = new Dictionary<string, List<string>>();
        // List to store final results
        var combinations = new List<List<string>>();

        // Recursive helper function to find combinations
        void FindCombinationsHelper(string currentAirport, List<string> currentFlightCodes, int connectionsMade)
        {
            // Check if destination is reached or connection limit exceeded
            if (currentAirport == destination || connectionsMade > maxConnections)
            {
                combinations.Add(new List<string>(currentFlightCodes));
                return;
            }

            // Find connected flights
            var connectedFlights = new HashSet<string>();
            foreach (var flight in flights)
            {
                if (flight.DepartureStation == currentAirport && !currentFlightCodes.Contains(flight.FlightNumber))
                {
                    connectedFlights.Add(flight.FlightNumber);
                }
            }

            // Recursively explore connected flights (avoiding duplicates)
            foreach (var flightCode in connectedFlights)
            {
                var newFlightCodes = new List<string>(currentFlightCodes) { flightCode };
                FindCombinationsHelper(flights.Single(f => f.FlightNumber == flightCode).ArrivalStation, newFlightCodes, connectionsMade + 1);
            }
        }

        // Start recursion from origin airport
        FindCombinationsHelper(origin, new List<string>(), 0);

        return combinations.Select(c => new JourneyRes()
        {
            Flights = c.Select(f => flights.Single(i => i.FlightNumber == f)).ToList()
        }).ToList();
    }
}