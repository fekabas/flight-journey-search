using BusinessLayer.BusinessLogic.DTOs.FlightDTOs;
using BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;

namespace BusinessLayer.BusinessLogic.Helpers.JourneyHelpers;

/// <summary>
/// Journey Calculator implementation that returns the flight combinations for the given origin and destination
/// </summary>
public class JourneyCalculator : IJourneyCalculator
{
    public JourneyCalculator()
    {
        
    }
    public List<JourneyRes> FindRoute(List<FlightItemRes> flights, string origin, string destination, uint maxLayovers = 1)
    {
        // Dictionary to store explored paths (origin -> [connected flights])
        var explored = new Dictionary<string, List<string>>();
        // List to store final results
        var combinations = new List<List<string>>();

        // Recursive helper function to find combinations
        void FindCombinationsHelper(string currentAirport, List<string> currentFlightCodes, int layoversMade)
        {
            // Check if destination is reached or connection limit exceeded
            if (currentAirport == destination && layoversMade <= maxLayovers)
            {
                combinations.Add(new List<string>(currentFlightCodes));
                return;
            } else if (layoversMade > maxLayovers)
            {
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
                FindCombinationsHelper(flights.Single(f => f.Transport.FlightNumber == flightCode).Destination, newFlightCodes, layoversMade + 1);
            }
        }

        // Start recursion from origin airport
        FindCombinationsHelper(origin, new List<string>(), 0);

        return combinations.Select(c => new JourneyRes()
        {
            Origin = origin,
            Destination = destination,
            Price = c.Select(f => flights.Single(i => i.Transport.FlightNumber == f)).Sum(f => f.Price),
            Flights = c.Select(f => flights.Single(i => i.Transport.FlightNumber == f)).ToList()
        }).ToList();
    }
}