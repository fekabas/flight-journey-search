using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Models.AbstractEntities;

namespace Entities.Models;

[Table("Transports")]
public class Transport : Entity
{
    public Transport()
    {
    }

    public string FlightCarrier { get; set; }

    public string FlightNumber { get; set; }
}