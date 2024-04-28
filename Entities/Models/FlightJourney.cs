using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class FlightJourney
{
    public FlightJourney()
    {
    }

    [ForeignKey(nameof(FlightId))]
    public Flight? Flight { get; set; }
    public Guid FlightId { get; set; }

    [ForeignKey(nameof(JourneyId))]
    public Journey? Journey { get; set; }
    public Guid JourneyId { get; set; }
}