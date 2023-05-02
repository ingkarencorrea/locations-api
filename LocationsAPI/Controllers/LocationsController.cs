using Microsoft.AspNetCore.Mvc;

namespace LocationsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILogger<LocationsController> _logger;

    public LocationsController(ILogger<LocationsController> logger)
    {
        _logger = logger;
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
}