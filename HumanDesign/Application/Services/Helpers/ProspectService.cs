using HumanDesign.Infrastructure.Entities;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Domain.Models.Requests;
using HumanDesign.Application.Interfaces;
using GeoTimeZone;

namespace HumanDesign.Application.Services.Helpers;
public class ProspectService(
    AppDbContext db,
    IHumanDesignCalculator calculator) : IProspectService
{
    private readonly AppDbContext _db = db;
    private readonly IHumanDesignCalculator _calculator = calculator;

    public async Task<Guid> CreateProspectAsync(CreateProspectRequest req, Guid ownerId)
    {
        var tz = TimeZoneLookup.GetTimeZone(req.Latitude, req.Longitude).Result;

        var birthUtc = ConvertToUtc(req.BirthDate, req.BirthTime, tz);

        var prospect = new Prospect
        {
            Id = Guid.NewGuid(),
            FullName = req.FullName,
            Email = req.Email,
            Phone = req.Phone,
            OwnerId = ownerId,
            BirthDateLocal = req.BirthDate + req.BirthTime,
            BirthDateUtc = birthUtc,
            BirthLocation = req.BirthLocation,
            Latitude = req.Latitude,
            Longitude = req.Longitude,
            TimeZone = tz
        };

        _db.Prospects.Add(prospect);

        // Generate HD Design via SharpAstrology
        var design = await _calculator.GenerateDesignAsync(prospect);

        prospect.GeneratedDesign = design;

        await _db.SaveChangesAsync();

        return prospect.Id;
    }
    private static DateTime ConvertToUtc(DateTime date, TimeSpan time, string tz)
    {
        var local = date.Date + time;
        var tzInfo = TimeZoneInfo.FindSystemTimeZoneById(tz);
        return TimeZoneInfo.ConvertTimeToUtc(local, tzInfo);
    }
}