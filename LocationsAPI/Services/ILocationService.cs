using LocationsAPI.Models;

namespace LocationsAPI.Services;

public interface ILocationService
{
    public Task<bool> CreateAsync(Location location);

    public Task<IEnumerable<Location>> GetAllAsync();
    public Task<IEnumerable<Location>> GetAvailableLocations(DateTime openTime, DateTime closeTime);
}