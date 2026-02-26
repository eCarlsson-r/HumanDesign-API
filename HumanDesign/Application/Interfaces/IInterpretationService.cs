using HumanDesign.Domain.Models.Reference;

namespace HumanDesign.Application.Interfaces;
public interface IInterpretationService
{
    Task<AttributeDetail> GetTypeAsync(string typeName);
    Task<AttributeDetail> GetAuthorityAsync(string authority);
    Task<AttributeDetail> GetDefinitionAsync(string definition);

    Task<AttributeDetail> GetProfileAsync(string profileCode);
    Task<AttributeDetail> GetCrossAsync(string crossCode);

    Task<List<AttributeDetail>> GetGatesAsync(Guid designId);
    Task<List<AttributeDetail>> GetCentersAsync(Guid designId);
}