using HumanDesign.Data;
namespace HumanDesign.Services;
public class FileResolver
{
    private readonly AppDbContext _db;

    public FileResolver(AppDbContext db)
    {
        _db = db;
    }

    public async Task<string?> ResolveImageAsync(int? fileNo)
    {
        if (!fileNo.HasValue) return null;

        var file = await _db.Files.FindAsync(fileNo.Value);

        if (file == null) return null;

        return $"{file.Path}/{file.FileName}.{file.Extension}";
    }
}