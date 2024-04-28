using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Models.AbstractEntities;

namespace Entities.Models;

[Table("Flights")]
public class Flight : Entity
{
    public Flight()
    {
    }

    [ForeignKey(nameof(TransportId))]
    public Transport? Transport { get; set; }
    public Guid TransportId { get; set; }

    public string Origin { get; set; }
    public string Destination { get; set; }
    public double Price { get; set; }
}