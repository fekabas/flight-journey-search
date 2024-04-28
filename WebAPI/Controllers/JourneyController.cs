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
    public async Task<ActionResult> Find(string origin, string destination, int take = 5)
    {
        List<JourneyRes>? journeys = await journeyBusinessLogic.CalculateJourneyAsync(origin, destination);

        if(journeys is null)
            return NoContent();
        else
            return Ok(new {
                Total = journeys.Count(),
                Journeys = journeys.OrderBy(j => j.Flights.Sum(f => f.Price)).Take(take)
            });
    }
}