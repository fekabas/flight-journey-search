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

    public string Origin { get; set; }
    public string Destination { get; set; }
    public double Price { get; set; }

    public List<FlightJourney> Flights { get; set; }
}