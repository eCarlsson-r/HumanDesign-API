
using HumanDesign.Domain.Models.Reference;

namespace HumanDesign.Application.Interfaces;

public interface IReferenceDataService
{
    Task<ReferenceDataBundle> GetBundleAsync();

    Task<AttributeDetail?> GetTypeAsync(string type);

    Task<AttributeDetail?> GetGateAsync(int gate);

    Task<AttributeDetail?> GetChannelAsync(int channelId);

    Task<AttributeDetail?> GetProfileAsync(string profile);

    Task<AttributeDetail?> GetCrossAsync(int cross);

    Task<AttributeDetail?> GetVariableAsync(string variableKey);
}