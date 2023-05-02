using LocationsAPI.Models;
using LocationsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LocationsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILogger<LocationsController> _logger;
    private ILocationService _locationService;

    public LocationsController(ILogger<LocationsController> logger, ILocationService locationService)
    {
        _logger = logger;
        _locationService = locationService;
    }

    [HttpGet(Name = "GetAllLocations")]
    public IEnumerable<Location> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Location()
            {
                Id = Random.Shared.Next(0, 50)
            })
            .ToArray();
    }
    
    [HttpPost(Name = "CreateLocation")]
    public async Task<IActionResult> Post(Location location)
    {
        var created = await _locationService.CreateAsync(location);
        return !created ? BadRequest(new { errorMessage = "Error creating the Location" }) 
            : Created($"/location/{location.Name}", location);
    }
}