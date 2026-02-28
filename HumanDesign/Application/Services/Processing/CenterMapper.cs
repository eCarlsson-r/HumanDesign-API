namespace HumanDesign.Application.Services.Processing;

public static class CenterMapper
{
    private static readonly Dictionary<string, string> Map = new()
    {
        ["Crown"] = "Head",
        ["Mind"] = "Ajna",
        ["Throat"] = "Throat",
        ["Self"] = "G",
        ["Heart"] = "Heart",
        ["Emotions"] = "SolarPlexus",
        ["Sacral"] = "Sacral",
        ["Spleen"] = "Spleen",
        ["Root"] = "Root"
    };
    
    public static string ToHdName(string sharpName)
    {
        return Map.TryGetValue(sharpName, out var hdName) ? hdName : sharpName;
    }
}