using HumanDesign.Domain.Models.Reports;

namespace HumanDesign.Application.Interfaces;
public interface IContentResolverService
{
    Task<TypeBundleResult> ResolveTypeBundleAsync(string typeName);

    Task<ResolvedContent?> ResolveProfileAsync(string profile);
    Task<ResolvedContent?> ResolveCrossAsync(string crossName);

    Task<ResolvedContent?> ResolveGateAsync(int gate);
    Task<ResolvedContent?> ResolveChannelAsync(int channelId);

    Task<ResolvedContent?> ResolveCenterAsync(string centerName, string definition);

    Task<ResolvedContent?> ResolveAttributeAsync(string property, string value);
}