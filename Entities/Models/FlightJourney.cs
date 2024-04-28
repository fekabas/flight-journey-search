using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class FlightJourney
{
    public FlightJourney()
    {
    }

    [ForeignKey(nameof(FlightId))]
    public Flight? Flight { get; set; }
    [Required]
    public Guid FlightId { get; set; }

    [ForeignKey(nameof(JourneyId))]
    public Journey? Journey { get; set; }
    [Required]
    public Guid JourneyId { get; set; }
}