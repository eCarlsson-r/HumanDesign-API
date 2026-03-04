using HumanDesign.Infrastructure.Entities.Reference;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using HumanDesign.Infrastructure.Data;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/cms/attributes")]
public class AttributeCmsController(AppDbContext db) : ControllerBase
{
    private readonly AppDbContext _db = db;

    [HttpGet("{id}")]
    public async Task<AttributeEntity> Get(int id) => await _db.Attributes.FindAsync(id);

    [HttpGet]
    public async Task<List<AttributeEntity>> GetAll([FromQuery] string? property)
    {
        var query = _db.Attributes.AsQueryable();

        if (!string.IsNullOrEmpty(property))
            query = query.Where(x => x.Property == property);

        return await query.ToListAsync();
    }

    // Update content only (not property/value)
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AttributeEntity dto)
    {
        var entity = await _db.Attributes.FindAsync(id);
        if (entity == null)
            return NotFound();

        entity.Title = dto.Title;
        entity.Preview = dto.Preview;
        entity.Summary = dto.Summary;
        entity.Detail = dto.Detail;
        entity.ImgNo = dto.ImgNo;

        await _db.SaveChangesAsync();

        return Ok();
    }
}