namespace HumanDesign.Application.Interfaces;

public interface IGeoService
{
    Task<(double lat, double lng, string timezone)>
        ResolveLocationAsync(string location);
}