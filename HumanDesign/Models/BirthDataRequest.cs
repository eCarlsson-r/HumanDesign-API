public class BirthDataRequest
{
    public DateTime BirthDateUtc { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string TimeZone { get; set; } = default!;
}