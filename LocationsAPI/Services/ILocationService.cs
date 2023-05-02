using LocationsAPI.Models;

namespace LocationsAPI.Services;

public interface ILocationService
{
    public Task<bool> CreateAsync(Location location);
}