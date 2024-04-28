using BusinessLayer.BusinessLogic.DTOs.JourneyDTOs;
using BusinessLayer.Interfaces;
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
    /// Get the cheapest flight to go from origin to destination
    /// </summary>
    [HttpGet("{origin}/{destination}")]
    public async Task<ActionResult<JourneyRes>> Find(string origin, string destination, int numberOfFlighs = 1)
    {
        JourneyRes? journey = await journeyBusinessLogic.GetCheapestFlightAsync(origin, destination, numberOfFlighs);

        if(journey is null)
            return NoContent();
        else
            return Ok(journey);
    }
}