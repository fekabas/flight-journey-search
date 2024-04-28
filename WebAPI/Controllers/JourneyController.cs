using BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;
using BusinessLayer.ExternalServices.DTOs.FlightAPIServiceDTOs;
using BusinessLayer.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/journey")]
[ApiController]
[Authorize]
public class JourneyController : Controller
{
    private readonly IJourneyBusinessLogic journeyBusinessLogic;

    public JourneyController(
        IJourneyBusinessLogic journeyBusinessLogic
    )
    {
        this.journeyBusinessLogic = journeyBusinessLogic;
    }

    /// <summary>
    /// Get flight that matches origin and destination
    /// </summary>
    [HttpGet("{origin}/{destination}")]
    public async Task<ActionResult<JourneyRes>> Find(string origin, string destination, int layovers = 1)
    {
        List<FlightCombinationRes>? journeyCombinations = await journeyBusinessLogic.GetCombinationsAsync(origin, destination, layovers);
        var cheapestJourney = journeyCombinations?.OrderBy(j => j.Flights!.Sum(f => f.Price)).FirstOrDefault();
        if(cheapestJourney is null)
            return NoContent();
        else
            return Ok(new JourneyRes()
            {
                Origin = origin,
                Destination = destination,
                Price = cheapestJourney.Flights!.Sum(f => f.Price),
                Flights = cheapestJourney.Flights
            });
    }
}