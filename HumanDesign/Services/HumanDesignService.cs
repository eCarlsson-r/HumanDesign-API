using HumanDesignApi.Models;
using SharpAstrology.DataModels;
using SharpAstrology.Enums;
using SharpAstrology.Interfaces;
using SharpAstrology.ExtensionMethods;

namespace HumanDesignApi.Services;

public class HumanDesignService
{
    private readonly IEphemerides _ephemerides;

    public HumanDesignService(IEphemerides ephemerides)
    {
        _ephemerides = ephemerides;
    }

    public LegacyHumanDesignResponse MapToLegacy(HumanDesign chart, string name, string dob, string tob)
    {
        var data = chart.ChartData;
        // Using the extension method to find design date
        var designDate = _ephemerides.DesignJulianDay(chart.pointInTime, EphCalculationMode.Tropic);

        var response = new LegacyHumanDesignResponse
        {
            Name = name,
            Dob = dob,
            Tob = tob,
            DesignDateTime = designDate.ToString("yyyy-MM-dd HH:mm:ss"),
            DesignType = ((int)data.Type).ToString(),
            TypeName = data.Type.ToText(),
            DesignAuthority = ((int)data.Strategy).ToString(),
            AuthorityName = data.Strategy.ToString(),
            DesignProfile = data.Profile.ToText().Replace(" ", ""), 
            DesignCross = ((int)data.IncarnationCross).ToString(),
            DesignCrossType = ((int)data.IncarnationCross.ToGeneKeys().Item2).ToString(),
            CrossName = data.IncarnationCross.ToText() + "(" + string.Join("/", GetCrossGates(data.IncarnationCross.ToGeneKeys().Item1)) + ")",
            DesignDigestion = data.DesignActivation[Planets.Sun].Color.ToNumber().ToString(),
            DesignReasoning = data.DesignActivation[Planets.Sun].Tone.ToNumber().ToString(),
            DesignCognition = data.DesignActivation[Planets.Earth].Tone.ToNumber().ToString(),
            DesignMotivation = data.PersonalityActivation[Planets.Sun].Color.ToNumber().ToString(),
            DesignPerspective = data.PersonalityActivation[Planets.Sun].Tone.ToNumber().ToString(),
            DesignEnvironment = data.DesignActivation[Planets.Sun].Color.ToNumber().ToString(),
            DesignDigestionArrow = FormatArrow(data.Variables.Digestion, data.DesignActivation[Planets.Sun]),
            DesignEnvironmentArrow = FormatArrow(data.Variables.Environment, data.DesignActivation[Planets.NorthNode]),
            DesignAwarenessArrow = FormatArrow(data.Variables.Awareness, data.PersonalityActivation[Planets.Sun]),
            DesignPerspectiveArrow = FormatArrow(data.Variables.Perspective, data.PersonalityActivation[Planets.NorthNode])
        };

        // Mapping Gates - using plural Planets
        response.DesignUc01 = FormatGate(data.DesignActivation[Planets.Sun], data.DesignFixation[Planets.Sun]);
        response.DesignUc02 = FormatGate(data.DesignActivation[Planets.Earth], data.DesignFixation[Planets.Earth]);
        response.DesignUc03 = FormatGate(data.DesignActivation[Planets.NorthNode], data.DesignFixation[Planets.NorthNode]);
        response.DesignUc04 = FormatGate(data.DesignActivation[Planets.SouthNode], data.DesignFixation[Planets.SouthNode]);
        response.DesignUc05 = FormatGate(data.DesignActivation[Planets.Moon], data.DesignFixation[Planets.Moon]);
        response.DesignUc06 = FormatGate(data.DesignActivation[Planets.Mercury], data.DesignFixation[Planets.Mercury]);
        response.DesignUc07 = FormatGate(data.DesignActivation[Planets.Venus], data.DesignFixation[Planets.Venus]);
        response.DesignUc08 = FormatGate(data.DesignActivation[Planets.Mars], data.DesignFixation[Planets.Mars]);
        response.DesignUc09 = FormatGate(data.DesignActivation[Planets.Jupiter], data.DesignFixation[Planets.Jupiter]);
        response.DesignUc10 = FormatGate(data.DesignActivation[Planets.Saturn], data.DesignFixation[Planets.Saturn]);
        response.DesignUc11 = FormatGate(data.DesignActivation[Planets.Uranus], data.DesignFixation[Planets.Uranus]);
        response.DesignUc12 = FormatGate(data.DesignActivation[Planets.Neptune], data.DesignFixation[Planets.Neptune]);
        response.DesignUc13 = FormatGate(data.DesignActivation[Planets.Pluto], data.DesignFixation[Planets.Pluto]);
        
        if (data.DesignActivation.ContainsKey(Planets.Chiron)) response.DesignUc14 = FormatGate(data.DesignActivation[Planets.Chiron], data.DesignFixation[Planets.Chiron]);

        // Centers mapping
        response.DesignKepala = IsCenterDefined(data, Centers.Crown) ? "AKTIF" : "RESEPTIF";
        response.DesignAjna = IsCenterDefined(data, Centers.Mind) ? "AKTIF" : "RESEPTIF";
        response.DesignTenggorokan = IsCenterDefined(data, Centers.Throat) ? "AKTIF" : "RESEPTIF";
        response.DesignJati = IsCenterDefined(data, Centers.Self) ? "AKTIF" : "RESEPTIF";
        response.DesignJantung = IsCenterDefined(data, Centers.Heart) ? "AKTIF" : "RESEPTIF";
        response.DesignPlexus = IsCenterDefined(data, Centers.Emotions) ? "AKTIF" : "RESEPTIF";
        response.DesignLimpa = IsCenterDefined(data, Centers.Spleen) ? "AKTIF" : "RESEPTIF";
        response.DesignSakral = IsCenterDefined(data, Centers.Sacral) ? "AKTIF" : "RESEPTIF";
        response.DesignKuda = IsCenterDefined(data, Centers.Root) ? "AKTIF" : "RESEPTIF";

        // Personality Gates - using plural Planets
        response.DesignC01 = FormatGate(data.PersonalityActivation[Planets.Sun], data.PersonalityFixation[Planets.Sun]);
        response.DesignC02 = FormatGate(data.PersonalityActivation[Planets.Earth], data.PersonalityFixation[Planets.Earth]);
        response.DesignC03 = FormatGate(data.PersonalityActivation[Planets.NorthNode], data.PersonalityFixation[Planets.NorthNode]);
        response.DesignC04 = FormatGate(data.PersonalityActivation[Planets.SouthNode], data.PersonalityFixation[Planets.SouthNode]);
        response.DesignC05 = FormatGate(data.PersonalityActivation[Planets.Moon], data.PersonalityFixation[Planets.Moon]);
        response.DesignC06 = FormatGate(data.PersonalityActivation[Planets.Mercury], data.PersonalityFixation[Planets.Mercury]);
        response.DesignC07 = FormatGate(data.PersonalityActivation[Planets.Venus], data.PersonalityFixation[Planets.Venus]);
        response.DesignC08 = FormatGate(data.PersonalityActivation[Planets.Mars], data.PersonalityFixation[Planets.Mars]);
        response.DesignC09 = FormatGate(data.PersonalityActivation[Planets.Jupiter], data.PersonalityFixation[Planets.Jupiter]);
        response.DesignC10 = FormatGate(data.PersonalityActivation[Planets.Saturn], data.PersonalityFixation[Planets.Saturn]);
        response.DesignC11 = FormatGate(data.PersonalityActivation[Planets.Uranus], data.PersonalityFixation[Planets.Uranus]);
        response.DesignC12 = FormatGate(data.PersonalityActivation[Planets.Neptune], data.PersonalityFixation[Planets.Neptune]);
        response.DesignC13 = FormatGate(data.PersonalityActivation[Planets.Pluto], data.PersonalityFixation[Planets.Pluto]);

        if (data.PersonalityActivation.ContainsKey(Planets.Chiron)) response.DesignC14 = FormatGate(data.PersonalityActivation[Planets.Chiron], data.PersonalityFixation[Planets.Chiron]);

        // Channels
        response.AdditionalData = new Dictionary<string, object>();
        int idx = 1;
        foreach (var channel in data.ActiveChannels)
        {
            int[] gates = [channel.ToGates().Item1.ToNumber(), channel.ToGates().Item2.ToNumber()];
            response.AdditionalData[$"design-channel{idx}"] = channel.ToText().Split(" ")[1];
            idx++;
        }

        return response;
    }

    private bool IsCenterDefined(HumanDesignChart chart, Centers center)
    {
        return chart.CenterActivations.ContainsKey(center) && chart.CenterActivations[center] != ActivationTypes.None;
    }

    private string FormatGate(dynamic activation, dynamic fixation)
    {
        return $"{GatesExtensionMethods.ToNumber(activation.Gate)}.{LinesExtensionMethods.ToNumber(activation.Line)}*{fixation.FixingState}";
    }

    private string FormatArrow(dynamic arrow, dynamic planet)
    {
        return $"{arrow.Orientation}:{ColorExtensionMethods.ToNumber(planet.Color)}*{ToneExtensionMethods.ToNumber(planet.Tone)}";
    }

    private int[] GetCrossGates(Gates gates)
    {
        var gatesList = GatesExtensionMethods.HarmonicGates(gates);
        int[] gateList = new int[gatesList.Length];

        for (var index = 0; index < gatesList.Length; index++)
        {
            gateList[index] = gatesList[index].ToNumber();
        }

        return gateList;
    }
}
