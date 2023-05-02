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

    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<Location>("SELECT * FROM Location");
    }
    
    public async Task<IEnumerable<Location>> GetAvailableLocations(TimeOnly openTimeReq, TimeOnly closeTimeReq)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<Location>("SELECT * FROM Location WHERE OpenTime <= @openTime AND CloseTime >= @closeTime",
            new {openTime = openTimeReq, closeTime = closeTimeReq});
    }
}