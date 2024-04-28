using BusinessLayer.BusinessLogic.DTOs.FlightDTOs;
using BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;

namespace BusinessLayer.BusinessLogic.Helpers.JourneyHelpers;

/// <summary>
/// Journey Calculator implementation that returns the first found flight combination
/// </summary>
public class JourneyCalculator : IJourneyCalculator
{
    public JourneyCalculator()
    {
        
    }
    public List<FlightCombinationRes> FindRoute(List<FlightItemRes> flights, string origin, string destination, int maxConnections = 1)
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
                if (flight.Origin == currentAirport && !currentFlightCodes.Contains(flight.Transport.FlightNumber))
                {
                    connectedFlights.Add(flight.Transport.FlightNumber);
                }
            }

            // Recursively explore connected flights (avoiding duplicates)
            foreach (var flightCode in connectedFlights)
            {
                var newFlightCodes = new List<string>(currentFlightCodes) { flightCode };
                FindCombinationsHelper(flights.Single(f => f.Transport.FlightNumber == flightCode).Destination, newFlightCodes, connectionsMade + 1);
            }
        }

        // Start recursion from origin airport
        FindCombinationsHelper(origin, new List<string>(), 0);

        return combinations.Select(c => new FlightCombinationRes()
        {
            Flights = c.Select(f => flights.Single(i => i.Transport.FlightNumber == f)).ToList()
        }).ToList();
    }
}