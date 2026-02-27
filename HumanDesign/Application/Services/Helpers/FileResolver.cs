using HumanDesign.Infrastructure.Data;
namespace HumanDesign.Application.Services.Helpers;
public class FileResolver(AppDbContext db)
{
    private readonly AppDbContext _db = db;

    public async Task<string?> ResolveImageAsync(int? fileNo)
    {
        if (!fileNo.HasValue) return null;

        var file = await _db.Files.FindAsync(fileNo.Value);

        if (file == null) return null;

        return $"{file.Path}/{file.FileName}.{file.Extension}";
    }
}