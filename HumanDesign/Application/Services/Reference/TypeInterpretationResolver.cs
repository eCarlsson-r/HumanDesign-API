using HumanDesign.Domain.Models.Reference;
using HumanDesign.Application.Interfaces;

namespace HumanDesign.Application.Services.Reference;

public class TypeInterpretationService(IInterpretationService interpretationService) : ITypeInterpretationService
{
    private readonly IInterpretationService _interpretationService = interpretationService;

    public async Task<AttributeDetail> GetStrategyAsync(string type)
    {
        var value = StrategyMap[type];
        return await _interpretationService.GetAttributeAsync("Strategy", value);
    }

    public async Task<AttributeDetail> GetSignatureAsync(string type)
    {
        var value = SignatureMap[type];
        return await _interpretationService.GetAttributeAsync("Signature", value);
    }

    public async Task<AttributeDetail> GetNotSelfThemeAsync(string type)
    {
        var value = NotSelfMap[type];
        return await _interpretationService.GetAttributeAsync("NotSelf", value);
    }

    private static readonly Dictionary<string, string> StrategyMap = new()
    {
        ["Generator"] = "To Respond",
        ["Manifestor"] = "Inform",
        ["Projector"] = "Wait for Invitation",
        ["Reflector"] = "Wait Lunar Cycle"
    };

    private static readonly Dictionary<string, string> SignatureMap = new()
    {
        ["Generator"] = "Satisfaction",
        ["Manifestor"] = "Peace",
        ["Projector"] = "Success",
        ["Reflector"] = "Surprise"
    };

    private static readonly Dictionary<string, string> NotSelfMap = new()
    {
        ["Generator"] = "Frustration",
        ["Manifestor"] = "Anger",
        ["Projector"] = "Bitterness",
        ["Reflector"] = "Disappointment"
    };
}