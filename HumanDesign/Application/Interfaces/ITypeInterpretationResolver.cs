using HumanDesign.Domain.Models.Reference;

namespace HumanDesign.Application.Interfaces;
public interface ITypeInterpretationService
{
    Task<AttributeDetail> GetStrategyAsync(string type);
    Task<AttributeDetail> GetSignatureAsync(string type);
    Task<AttributeDetail> GetNotSelfThemeAsync(string type);
}