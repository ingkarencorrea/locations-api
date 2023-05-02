using LocationsAPI.Models;
using LocationsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LocationsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILogger<LocationsController> _logger;
    private readonly ILocationService _locationService;

    public LocationsController(ILogger<LocationsController> logger, ILocationService locationService)
    {
        _logger = logger;
        _locationService = locationService;
    }

    [HttpGet(Name = "GetAllLocations")]
    public async Task<IActionResult> Get()
    {
        var locations = await _locationService.GetAllAsync();
        return Ok(locations);
    }
    
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableLocations([FromQuery] DateTime openTime, [FromQuery] DateTime closeTime)
    {
        var availableLocations = await _locationService.GetAvailableLocations(openTime, closeTime);
        return Ok(availableLocations);
    }
    
    [HttpPost(Name = "CreateLocation")]
    public async Task<IActionResult> Post(Location location)
    {
        var created = await _locationService.CreateAsync(location);
        return !created ? BadRequest(new { errorMessage = "Error creating the Location" }) 
            : Created($"/location/{location.Name}", location);
    }
}