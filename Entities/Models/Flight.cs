using System.ComponentModel.DataAnnotations;
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
    [Required]
    public Guid TransportId { get; set; }

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
}