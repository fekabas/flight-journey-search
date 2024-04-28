using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer;
using DataLayer.Models.AbstractEntities;

namespace Entities.Models;

[Table("Journeys")]
public class Journey : Entity
{
    public Journey()
    {
    }

    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string Origin { get; set; }
    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string Destination { get; set; }
    [Range(0, 10000)]
    public double Price { get; set; }

    [MinLength(1)]
    public List<FlightJourney>? Flights { get; set; }
}