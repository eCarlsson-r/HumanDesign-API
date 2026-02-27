using HumanDesign.Infrastructure.Entities;
using HumanDesign.Infrastructure.Entities.Charts;
using HumanDesign.Application.Interfaces;
using SharpAstrology.DataModels;
using SharpAstrology.Ephemerides;
using SharpAstrology.Interfaces;
using SharpAstrology.Enums;
using HumanDesign.Domain.Enums;
using HumanDesign.Application.Services.Diagram;
using HumanDesign.Domain.Models.Diagram;

namespace HumanDesign.Application.Services.Processing;
public class HumanDesignCalculator : IHumanDesignCalculator
{
    private static List<DefinedChannel> MapChannels(HumanDesignChart chart)
    {
        var channels = new List<DefinedChannel>();

        foreach (var channel in chart.ActiveChannels)
        {
            var gates = channel.ToGates();

            channels.Add(new DefinedChannel
            {
                GateA = gates.Item1.ToNumber(),
                GateB = gates.Item2.ToNumber(),
                ChannelId = channel.ToText().Replace("Channel ", "")
            });
        }

        return channels;
    }

    private static List<PlanetaryActivation> MapActivations(HumanDesignChart chart)
    {
        var activations = new List<PlanetaryActivation>();

        foreach (var (planet, activation) in chart.PersonalityActivation)
        {
            activations.Add(new PlanetaryActivation
            {
                Type = "Personality",
                Planet = planet.ToName(),
                Gate = activation.Gate.ToNumber(),
                Line = activation.Line.ToNumber(),
                Color = activation.Color.ToNumber(),
                Tone = activation.Tone.ToNumber(),
                Base = activation.Base.ToNumber()
            });
        }

        foreach (var (planet, activation) in chart.DesignActivation)
        {
            activations.Add(new PlanetaryActivation
            {
                Type = "Design",
                Planet = planet.ToName(),
                Gate = activation.Gate.ToNumber(),
                Line = activation.Line.ToNumber(),
                Color = activation.Color.ToNumber(),
                Tone = activation.Tone.ToNumber(),
                Base = activation.Base.ToNumber()
            });
        }

        return activations;
    }

    private static List<CenterDefinition> MapCenters(HumanDesignChart chart)
    {
        var centers = new List<CenterDefinition>();

        foreach (var (center, activation) in chart.CenterActivations)
        {
            centers.Add(new CenterDefinition
            {
                CenterName = CenterMapper.ToHdName(center.ToString()),
                Definition = activation != ActivationTypes.None ? "defined" : "undefined"
            });
        }

        return centers;
    }

    private static VariableSet MapVariables(HumanDesignChart chart)
    {
        var digestion = (Digestion)chart.DesignActivation[Planets.Sun].Color.ToNumber();
        var sense = (Sense)chart.DesignActivation[Planets.Sun].Tone.ToNumber();
        var designSense = (Sense)chart.DesignActivation[Planets.Earth].Tone.ToNumber();
        var motivation = (Motivation)chart.PersonalityActivation[Planets.Sun].Color.ToNumber();
        var perspective = (Perspective)chart.PersonalityActivation[Planets.Sun].Tone.ToNumber();
        var environment = (Domain.Enums.Environment)chart.DesignActivation[Planets.Sun].Color.ToNumber();

        return new VariableSet
        {
            Digestion = digestion.ToString(),
            Reasoning = sense.ToString(),
            Cognition = designSense.ToString(),
            Motivation = motivation.ToString(),
            Perspective = perspective.ToString(),
            Environment = environment.ToString(),
            DigestionArrow = MapVar(chart.Variables.Digestion),
            EnvironmentArrow = MapVar(chart.Variables.Environment),
            AwarenessArrow = MapVar(chart.Variables.Awareness),
            PerspectiveArrow = MapVar(chart.Variables.Perspective)
        };
    }

    private static VariableArrow MapVar(Variable v)
    {
        return new VariableArrow
        {
            IsLeft = v.Orientation == Orientation.Left,
            Color = v.Color.ToNumber(),
            Tone = v.Tone.ToNumber(),
            Base = v.Base.ToNumber()
        };
    }

    public async Task<Design> GenerateDesignAsync(Prospect prospect)
    {
        var ephemeridesService = new SwissEphemeridesService(ephType: EphType.Moshier);
        using IEphemerides eph = ephemeridesService.CreateContext();
        // Calculate the chart
        var chart = new HumanDesignChart(prospect.BirthDateUtc, eph);

        var design = new Design
        {
            Id = Guid.NewGuid(),
            ProspectId = prospect.Id,

            Type = chart.Type.ToText(),
            Authority = chart.Strategy.ToString(),
            Definition = chart.SplitDefinition.ToText(),
            Profile = chart.Profile.ToText(),
            IncarnationCross = (int)chart.IncarnationCross,

            Variables = MapVariables(chart),
            CenterDefinitions = MapCenters(chart),
            Channels = MapChannels(chart),
            Activations = MapActivations(chart)
        };

        return design;
    }
}