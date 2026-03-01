using System.Text.Json;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Infrastructure.Entities.Reference;
using Microsoft.EntityFrameworkCore;

namespace HumanDesign.Infrastructure.Persistence.Seed;

class TypeSeed
{
    public string Name { get; set; } = "";
    public int Strategy { get; set; }
    public int Characteristic { get; set; }
    public int Theme { get; set; }
    public string Preview { get; set; } = "";
    public string Summary { get; set; } = "";
    public string Detail { get; set; } = "";
}

public class ReferenceDataSeeder(AppDbContext db, IWebHostEnvironment env)
{
    private readonly AppDbContext _db = db;
    private readonly IWebHostEnvironment _env = env;

    public async Task SeedAsync()
    {
        await SeedAttributes();
        await SeedTypes();
        await SeedProfiles();
        await SeedGates();
        await SeedChannels();
        await SeedCenters();
        await SeedCrosses();
    }

    private string PathOf(string file)
        => Path.Combine(_env.ContentRootPath, "Infrastructure/Persistence/Seed/SeedData", file);

    private async Task SeedAttributes()
    {
        if (_db.Attributes.Any()) return;

        var json = await File.ReadAllTextAsync(PathOf("attributes.json"));
        var items = JsonSerializer.Deserialize<List<AttributeEntity>>(json)!;

        _db.Attributes.AddRange(items);
        await _db.SaveChangesAsync();
    }

    private async Task SeedTypes()
    {
        if (_db.Types.Any()) return;

        var json = await File.ReadAllTextAsync(PathOf("types.json"));
        var raw = JsonSerializer.Deserialize<List<TypeSeed>>(json)!;

        foreach (var t in raw)
        {
            var strategyId = await _db.Attributes
                .Where(a => a.Property == "Strategy" && a.Id == t.Strategy)
                .Select(a => a.Id)
                .FirstAsync();

            var characteristicId = await _db.Attributes
                .Where(a => a.Property == "Signature" && a.Id == t.Characteristic)
                .Select(a => a.Id)
                .FirstAsync();

            var themeId = await _db.Attributes
                .Where(a => a.Property == "NotSelf" && a.Id == t.Theme)
                .Select(a => a.Id)
                .FirstAsync();

            _db.Types.Add(new TypeEntity
            {
                Name = t.Name,
                Strategy = strategyId,
                Characteristic = characteristicId,
                Theme = themeId,
                Preview = t.Preview,
                Summary = t.Summary,
                Detail = t.Detail
            });
        }

        await _db.SaveChangesAsync();
    }

    private async Task SeedGates()
    {
        if (_db.Gates.Any()) return;

        var json = await File.ReadAllTextAsync(PathOf("gates.json"));
        var items = JsonSerializer.Deserialize<List<GateEntity>>(json)!;

        _db.Gates.AddRange(items);
        await _db.SaveChangesAsync();
    }

    private async Task SeedChannels()
    {
        if (_db.Channels.Any()) return;

        var json = await File.ReadAllTextAsync(PathOf("channels.json"));
        var items = JsonSerializer.Deserialize<List<ChannelEntity>>(json)!;

        _db.Channels.AddRange(items);
        await _db.SaveChangesAsync();
    }

    private async Task SeedProfiles()
    {
        if (_db.Profiles.Any()) return;

        var json = await File.ReadAllTextAsync(PathOf("profiles.json"));
        var items = JsonSerializer.Deserialize<List<ProfileEntity>>(json)!;

        _db.Profiles.AddRange(items);
        await _db.SaveChangesAsync();
    }

    private async Task SeedCenters()
    {
        if (_db.Centers.Any()) return;

        var json = await File.ReadAllTextAsync(PathOf("centers.json"));
        var items = JsonSerializer.Deserialize<List<CenterEntity>>(json)!;

        _db.Centers.AddRange(items);
        await _db.SaveChangesAsync();
    }

    private async Task SeedCrosses()
    {
        if (_db.Crosses.Any()) return;

        var json = await File.ReadAllTextAsync(PathOf("crosses.json"));
        var items = JsonSerializer.Deserialize<List<CrossEntity>>(json)!;

        _db.Crosses.AddRange(items);
        await _db.SaveChangesAsync();
    }
}