namespace LocationsAPI.Models;

public class Location
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Type { get; set; }
    public DateTime OpenTime { get; set; }
    public DateTime CloseTime { get; set; }
}