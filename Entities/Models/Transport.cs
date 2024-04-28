using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Models.AbstractEntities;

namespace Entities.Models;

[Table("Transports")]
public class Transport : Entity
{
    public Transport()
    {
    }
    
    [Required]
    [MaxLength(100)]
    [MinLength(1)]
    public string FlightCarrier { get; set; }
    
    [Required]
    [MaxLength(100)]
    [MinLength(1)]
    public string FlightNumber { get; set; }
}