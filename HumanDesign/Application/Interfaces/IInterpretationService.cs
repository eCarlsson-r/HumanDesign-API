using HumanDesign.Domain.Models.Charts;
using HumanDesign.Domain.Models.Reference;

namespace HumanDesign.Application.Interfaces;
public interface IInterpretationService
{
    Task<AttributeDetail> GetAttributeAsync(string property, string value);
    Task<AttributeDetail> GetTypeAsync(string typeName);
    Task<AttributeDetail> GetAuthorityAsync(string authority);
    Task<AttributeDetail> GetDefinitionAsync(string definition);

    Task<AttributeDetail> GetProfileAsync(string profileCode);
    Task<AttributeDetail> GetCrossAsync(int crossCode);

    Task<List<AttributeDetail>> GetGatesAsync(Guid DesignId);
    Task<List<CenterState>> GetCentersAsync(Guid DesignId);
}