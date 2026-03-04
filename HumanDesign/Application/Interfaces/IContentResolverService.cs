using HumanDesign.Domain.Models.Reports;

namespace HumanDesign.Application.Interfaces;
public interface IContentResolverService
{
    Task<TypeBundleResult> ResolveTypeBundleAsync(string typeName, string level);

    Task<ResolvedContent?> ResolveProfileAsync(string profile, string level);
    Task<ResolvedContent?> ResolveCrossAsync(string crossName, string level);

    Task<ResolvedContent?> ResolveGateAsync(int gate, string level);
    Task<ResolvedContent?> ResolveChannelAsync(int channelId, string level);

    Task<ResolvedContent?> ResolveCenterAsync(string centerName, string definition, string level);

    Task<ResolvedContent?> ResolveAttributeAsync(string property, string value, string level);
}