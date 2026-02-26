using HumanDesign.Models;
using HumanDesign.Data;
using HumanDesign.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HumanDesign.Services.Implementations;
public class InterpretationService(AppDbContext db, FileResolver fileResolver, IReferenceDataService referenceDataService) : IInterpretationService
{
    private readonly AppDbContext _db = db;
    private readonly FileResolver _fileResolver = fileResolver;
    private readonly IReferenceDataService _referenceDataService = referenceDataService;

    public async Task<AttributeDetail> GetTypeAsync(string typeName)
    {
        var entity = await _db.Types
            .FirstOrDefaultAsync(t => t.TypeName == typeName);

        if (entity == null) return new AttributeDetail();

        return new AttributeDetail
        {
            Title = entity.TypeName ?? "",
            Preview = entity.TypePreview,
            Summary = entity.TypeSummary,
            Detail = entity.TypeDetail,
            ImageUrl = await _fileResolver.ResolveImageAsync(entity.TypeImgNo)
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

    public async Task<List<AttributeDetail>> GetGatesAsync(Guid designId)
    {
        var gates = await _db.PlanetaryActivations
            .Where(a => a.DesignId == designId)
            .Select(a => a.Gate)
            .Distinct()
            .ToListAsync();

        var results = new List<AttributeDetail>();

        foreach (var gate in gates)
            results.Add(await _referenceDataService.GetGateAsync(gate, _fileResolver));

        return results;
    }

    // ================= PROFILE =================

    public async Task<AttributeDetail> GetProfileAsync(string profileCode)
    {
        var entity = await _db.Profiles
            .FirstOrDefaultAsync(p =>
                p.ProfileCode1 + "/" + p.ProfileCode2 == profileCode);

        if (entity == null) return new AttributeDetail();

        return new AttributeDetail
        {
            Title = entity.ProfileName ?? "",
            Summary = entity.ProfileSummary,
            Detail = entity.ProfileDetail,
            ImageUrl = await _fileResolver.ResolveImageAsync(entity.ProfileImgNo)
        };
    }

    // ================= CROSS =================

    public async Task<AttributeDetail> GetCrossAsync(string crossCode)
    {
        var entity = await _db.Crosses.FindAsync(crossCode);
        if (entity == null) return new AttributeDetail();

        return new AttributeDetail
        {
            Title = entity.CrossName ?? "",
            Summary = entity.CrossType,
            ImageUrl = null
        };
    }

    public async Task<List<CenterActivation>> GetCentersAsync(Guid designId)
    {
        return await _db.CenterDefinitions
            .Where(c => c.DesignId == designId)
            .Select(c => new CenterActivation
            {
                Name = c.CenterName,
                IsDefined = c.Definition == "defined"
            })
            .ToListAsync();
    }
}