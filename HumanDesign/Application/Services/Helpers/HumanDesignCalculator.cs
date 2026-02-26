using HumanDesign.Infrastructure.Entities;
using HumanDesign.Infrastructure.Entities.Charts;
using HumanDesign.Application.Interfaces;
using SharpAstrology.DataModels;
using SharpAstrology.Ephemerides;
using SharpAstrology.Interfaces;
using SharpAstrology.Enums;

namespace HumanDesign.Application.Services.Helpers;
public class HumanDesignCalculator : IHumanDesignCalculator
{
    
    public async Task<Design> GenerateDesignAsync(Prospect prospect)
    {
        var ephemeridesService = new SwissEphemeridesService(ephType: EphType.Moshier);
        using IEphemerides eph = ephemeridesService.CreateContext();

        var pointInTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        // Calculate the chart
        var chart = new HumanDesignChart(pointInTime, eph);

        return new Design{
            ProspectId = prospect.Id,
            Type = chart.Type.ToText(),
            Authority = chart.Strategy.ToString(),
            Definition = chart.SplitDefinition.ToText(),
            Profile = chart.Profile.ToText(),
            IncarnationCross = (int)chart.IncarnationCross,
            Centers = chart.CenterActivations,
            Activations = [chart.PersonalityActivation, chart.DesignActivation],
            Channels = chart.ActiveChannels,
            Variables = chart.Variables
        };
    }
}