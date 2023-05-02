using Dapper;
using LocationsAPI.Data;
using LocationsAPI.Models;

namespace LocationsAPI.Services;

public class LocationService : ILocationService
{
    private readonly IDbConnectionFactory _connectionFactory;

    public LocationService(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(Location location)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"INSERT INTO  Location (Name, Address, Latitude, Longitude, Type, OpenTime, CloseTime)
                 VALUES (@Name, @Address, @Latitude, @Longitude, @Type, @OpenTime, @CloseTime)", location);
        return result > 0;
    }
}