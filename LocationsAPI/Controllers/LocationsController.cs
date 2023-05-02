using System.Globalization;
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
        if (!locations.Any())
        {
            return NotFound("No locations found");
        }
        return Ok(locations);
    }
    
    [HttpPost("available")]
    public async Task<IActionResult> GetAvailableLocations([FromBody] LocationFilter filter)
    {
        if ( !(TimeOnly.TryParseExact(filter.OpenTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var openTimeOnly) ))
        {
            return BadRequest("Invalid OpenTime. Please use HH:mm format");
        }
        if ( !(TimeOnly.TryParseExact(filter.CloseTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var closeTimeOnly) ))
        {
            return BadRequest("Invalid CloseTime. Please use HH:mm format");
        }
        
        var availableLocations = await _locationService.GetAvailableLocations(openTimeOnly, closeTimeOnly);
        if (!availableLocations.Any())
        {
            return NotFound("No locations available for the given times");
        }
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