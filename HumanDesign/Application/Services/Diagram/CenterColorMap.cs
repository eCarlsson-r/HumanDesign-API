namespace HumanDesign.Application.Services.Diagram;

public static class CenterColorMap
{
    public static readonly Dictionary<string, string> DefinedColors = new()
    {
        ["Head"] = "#8A02B0",
        ["Ajna"] = "#064AFB",
        ["Throat"] = "#03D5F2",
        ["G"] = "#FDF500",
        ["Heart"] = "#F4A6B8",
        ["SolarPlexus"] = "#FDB304",
        ["Sacral"] = "#FF1a00",
        ["Spleen"] = "#79DC04",
        ["Root"] = "#954D02"
    };

    public const string UndefinedColor = "#FFFFFF";
}