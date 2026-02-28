using Microsoft.EntityFrameworkCore;
using HumanDesign.Infrastructure.Entities;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Domain.Models.Requests;
using HumanDesign.Application.Interfaces;
using HumanDesign.Infrastructure.Entities.Charts;

namespace HumanDesign.Application.Services.Helpers;
public class ProspectService(
    AppDbContext db,
    IGeoService geo,
    IHumanDesignCalculator calculator) : IProspectService
{
    private readonly AppDbContext _db = db;
    private readonly IGeoService _geo = geo;
    private readonly IHumanDesignCalculator _calculator = calculator;

    public async Task<Guid> CreateProspectAsync(CreateProspectRequest req)
    {
        var (lat, lng, tz) = await _geo.ResolveLocationAsync(req.BirthLocation);

        var birthUtc = ConvertToUtc(req.BirthDate, req.BirthTime, tz);

        var prospect = new Prospect
        {
            Id = Guid.NewGuid(),
            FullName = req.FullName,
            BirthDateLocal = req.BirthDate + req.BirthTime,
            BirthDateUtc = birthUtc,
            BirthLocation = req.BirthLocation,
            Latitude = lat,
            Longitude = lng,
            TimeZone = tz
        };

        _db.Prospects.Add(prospect);

        // Generate HD Design via SharpAstrology
        var design = await _calculator.GenerateDesignAsync(prospect);

        prospect.GeneratedDesign = design;

        await _db.SaveChangesAsync();

        return prospect.Id;
    }

    public async Task<Design> GetReportAsync(Guid id)
    {
        var designData = await _db.Designs
            .Include(d => d.Prospect)
            .Include(d => d.Activations)
            .Include(d => d.Channels)
            .Include(d => d.Variables)
            .Include(d => d.Variables.DigestionArrow)
            .Include(d => d.Variables.EnvironmentArrow)
            .Include(d => d.Variables.AwarenessArrow)
            .Include(d => d.Variables.PerspectiveArrow)
            .Include(d => d.CenterDefinitions)
            .Where(d => d.ProspectId == id).Select(p => new Design
                {
                    Id = p.Id,
                    ProspectId = id,
                    Prospect = p.Prospect,
                    Type = p.Type,
                    Authority = p.Authority,
                    Definition = p.Definition,
                    Profile = p.Profile,
                    IncarnationCross = p.IncarnationCross,
                    Activations = p.Activations,
                    Channels = p.Channels,
                    Variables = p.Variables,
                    CenterDefinitions = p.CenterDefinitions
                }
            ).FirstOrDefaultAsync();

        return designData!;
    }

    private static DateTime ConvertToUtc(DateTime date, TimeSpan time, string tz)
    {
        var local = date.Date + time;
        var tzInfo = TimeZoneInfo.FindSystemTimeZoneById(tz);
        return TimeZoneInfo.ConvertTimeToUtc(local, tzInfo);
    }
}