using HumanDesign.Domain.Models.Reference;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Application.Interfaces;
using HumanDesign.Application.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using HumanDesign.Domain.Models.Charts;

namespace HumanDesign.Application.Services.Reference;

public class InterpretationService(AppDbContext db, FileResolver fileResolver, IReferenceDataService referenceDataService) : IInterpretationService
{
    private readonly AppDbContext _db = db;
    private readonly FileResolver _fileResolver = fileResolver;
    private readonly IReferenceDataService _referenceDataService = referenceDataService;

    public async Task<AttributeDetail> GetTypeAsync(string typeName)
    {
        var entity = await _db.Types
            .FirstOrDefaultAsync(t => t.Name == typeName);

        if (entity == null) return new AttributeDetail();

        return new AttributeDetail
        {
            Title = entity.Name ?? "",
            Preview = entity.Preview,
            Summary = entity.Summary,
            Detail = entity.Detail,
            ImageUrl = await _fileResolver.ResolveImageAsync(entity.FileId)
        };
    }

    public async Task<AttributeDetail> GetAuthorityAsync(string authority)
    {
        var entity = await _db.Attributes
            .FirstOrDefaultAsync(a =>
                a.Property == "Authority" && a.Value == authority);

        if (entity == null) return new AttributeDetail();

        return new AttributeDetail
        {
            Title = entity.Value ?? "",
            Preview = entity.Preview,
            Summary = entity.Summary,
            Detail = entity.Detail,
            ImageUrl = await _fileResolver.ResolveImageAsync(entity.ImgNo)
        };
    }

    // ================= ATTRIBUTE =================

    public async Task<AttributeDetail> GetAttributeAsync(string property, string value)
    {
        var entity = await _db.Attributes
            .FirstOrDefaultAsync(a =>
                a.Property == property && a.Value == value);

        if (entity == null) return new AttributeDetail();

        return new AttributeDetail
        {
            Title = entity.Value ?? "",
            Preview = entity.Preview,
            Summary = entity.Summary,
            Detail = entity.Detail,
            ImageUrl = await _fileResolver.ResolveImageAsync(entity.ImgNo)
        };
    }

    public async Task<AttributeDetail> GetDefinitionAsync(string definition)
    {
        return await GetAttributeAsync("Definition", definition);
    }

    public async Task<List<AttributeDetail>> GetChannelsAsync(Guid designId)
    {
        var channels = await _db.DefinedChannels
            .Where(c => c.DesignId == designId)
            .Select(c => c.ChannelId)
            .Distinct()
            .ToListAsync();

        var results = new List<AttributeDetail>();

        foreach (var channel in channels)
        {
            var detail = await _referenceDataService.GetChannelAsync(int.Parse(channel));
            if (detail != null)
                results.Add(detail);
        }

        return results;
    }

    public async Task<List<AttributeDetail>> GetGatesAsync(Guid designId)
    {
        var gates = await _db.PlanetaryActivations
            .Where(a => a.DesignId == designId)
            .Select(a => a.Gate)
            .Distinct()
            .ToListAsync();

        var results = new List<AttributeDetail>();

        foreach (var gate in gates)
        {
            var detail = await _referenceDataService.GetGateAsync(gate);
            if (detail != null)
                results.Add(detail);
        }

        return results;
    }

    // ================= PROFILE =================

    public async Task<AttributeDetail> GetProfileAsync(string profileCode)
    {
        var entity = await _db.Profiles
            .FirstOrDefaultAsync(p =>
                p.Code1 + "/" + p.Code2 == profileCode);

        if (entity == null) return new AttributeDetail();

        return new AttributeDetail
        {
            Title = entity.Name,
            Summary = entity.Summary,
            Detail = entity.Detail,
            ImageUrl = await _fileResolver.ResolveImageAsync(entity.FileId)
        };
    }

    // ================= CROSS =================

    public async Task<AttributeDetail> GetCrossAsync(int crossCode)
    {
        var entity = await _db.Crosses.FindAsync(crossCode);
        if (entity == null) return new AttributeDetail();

        return new AttributeDetail
        {
            Title = entity.Name ?? "",
            Summary = entity.Type,
            ImageUrl = null
        };
    }

    public async Task<List<CenterState>> GetCentersAsync(Guid designId)
    {
        return await _db.CenterDefinitions
            .Where(c => c.DesignId == designId)
            .Select(c => new CenterState
            {
                Name = c.CenterName,
                IsDefined = c.Definition == "defined"
            })
            .ToListAsync();
    }
}