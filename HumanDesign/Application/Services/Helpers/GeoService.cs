using GeoTimeZone;
using HumanDesign.Application.Interfaces;
using Nominatim.API.Geocoders;
using Nominatim.API.Web;

namespace HumanDesign.Application.Services.Helpers;
public class GeoService(IHttpClientFactory http) : IGeoService
{
    private readonly IHttpClientFactory _http = http;

    public async Task<(double lat, double lng, string timezone)> ResolveLocationAsync(string location)
    {
        ForwardGeocoder searcher = new(new NominatimWebInterface(_http));

        var results = await searcher.Geocode(new Nominatim.API.Models.ForwardGeocodeRequest
        {
            queryString = location,
            LimitResults = 1
        });

        if (results == null || results.Length == 0)
            throw new Exception("Location not found");

        var lat = results[0].Latitude;
        var lng = results[0].Longitude;

        var tz = TimeZoneLookup.GetTimeZone(lat, lng).Result;

        return (lat, lng, tz);
    }
}