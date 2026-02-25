using SharpAstrology.DataModels;
using SharpAstrology.Enums;
using SharpAstrology.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HumanDesignApi;

public class HumanDesignData
{
    public required string Type { get; set; }
    public required string Profile { get; set; }
    public required string Authority { get; set; }
    public required string SplitDefinition { get; set; }
    public required string IncarnationCross { get; set; }
    public required List<string> Channels { get; set; }
    public required List<string> PersonalityGates { get; set; }
    public required List<string> DesignGates { get; set; }
    public required string DigestionArrow { get; set; }
    public required string PerspectiveArrow { get; set; }
    public required string EnvironmentArrow { get; set; }
    public required string AwarenessArrow { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class HumanDesign
{
    public HumanDesignChart ChartData => data;
    public HumanDesignChart data;
    public DateTime pointInTime;
    public HumanDesign(IEphemerides ephemerides, string dateString, string timezone)
    {
        var parts = dateString.Split("-");
        var year = int.Parse(parts[0]);
        var month = int.Parse(parts[1]);
        var day = int.Parse(parts[2]);

        DateTime specificTime = new(year, month, day, 0, 0, 0, DateTimeKind.Unspecified);
        TimeZoneInfo specifiedTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
        pointInTime = TimeZoneInfo.ConvertTimeToUtc(specificTime, specifiedTimeZone);

        data = new HumanDesignChart(pointInTime, ephemerides);
    }

    public HumanDesign(IEphemerides ephemerides, string dateString, string timeString, string timezone)
    {
        var dateParts = dateString.Split("-");
        var year = int.Parse(dateParts[0]);
        var month = int.Parse(dateParts[1]);
        var day = int.Parse(dateParts[2]);

        var timeParts = timeString.Split(":");
        var hour = int.Parse(timeParts[0]);
        var minute = int.Parse(timeParts[1]);

        DateTime specificTime = new(year, month, day, hour, minute, 0, DateTimeKind.Unspecified);
        TimeZoneInfo specifiedTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
        pointInTime = TimeZoneInfo.ConvertTimeToUtc(specificTime, specifiedTimeZone);

        data = new HumanDesignChart(pointInTime, ephemerides);
    }

    public HumanDesign(IEphemerides ephemerides, DateTime pointInTime)
    {
        data = new HumanDesignChart(pointInTime, ephemerides);
    }

    public string ToJSONString()
    {
        var result = new HumanDesignData()
        {
            Type = data.Type.ToText(),
            Profile = data.Profile.ToText(),
            Authority = data.Strategy.ToString(),
            SplitDefinition = data.SplitDefinition.ToText(),
            IncarnationCross = data.IncarnationCross.ToText(),
            Channels = data.ActiveChannels.Select(c => c.ToText()).ToList(),
            PersonalityGates = data.PersonalityActivation.Select(pa => $"{pa.Key} {pa.Value.Gate}.{pa.Value.Line}*{data.PersonalityFixation[pa.Key].FixingState}").ToList(),
            DesignGates = data.DesignActivation.Select(da => $"{da.Key} {da.Value.Gate}.{da.Value.Line}*{data.DesignFixation[da.Key].FixingState}").ToList(),
            DigestionArrow = $"{data.Variables.Digestion.Orientation}, {data.Variables.Digestion.Color.ToNumber()}-{data.Variables.Digestion.Tone.ToNumber()}",
            PerspectiveArrow = $"{data.Variables.Perspective.Orientation}, {data.Variables.Perspective.Color.ToNumber()}-{data.Variables.Perspective.Tone.ToNumber()}",
            EnvironmentArrow = $"{data.Variables.Environment.Orientation}, {data.Variables.Environment.Color.ToNumber()}-{data.Variables.Environment.Tone.ToNumber()}",
            AwarenessArrow = $"{data.Variables.Awareness.Orientation}, {data.Variables.Awareness.Color.ToNumber()}-{data.Variables.Awareness.Tone.ToNumber()}",
        };

        return JsonSerializer.Serialize(new { report = result, data });
    }
}