using HumanDesign.Infrastructure.Entities.Reference;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using HumanDesign.Infrastructure.Data;

namespace HumanDesign.API.Controllers;
[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/cms")]
public class AttributeCmsController(AppDbContext db) : ControllerBase
{
    private readonly AppDbContext _db = db;

    [HttpGet("attributes/{id}")]
    public async Task<AttributeEntity> Get(int id) => await _db.Attributes.FindAsync(id);

    [HttpGet("attributes")]
    public async Task<List<AttributeEntity>> GetAll()
    {
        var query = _db.Attributes.AsQueryable();

        return await query.ToListAsync();
    }

    [HttpGet("type")]
    public async Task<List<TypeEntity>> GetAllTypes()
    {
        var query = _db.Types.AsQueryable();

        return await query.ToListAsync();
    }

    [HttpGet("profile")]
    public async Task<List<ProfileEntity>> GetAllProfiles()
    {
        var query = _db.Profiles.AsQueryable();

        return await query.ToListAsync();
    }

    [HttpGet("gate")]
    public async Task<List<GateEntity>> GetAllGates()
    {
        var query = _db.Gates.AsQueryable();

        return await query.ToListAsync();
    }

    [HttpGet("channel")]
    public async Task<List<ChannelEntity>> GetAllChannels()
    {
        var query = _db.Channels.AsQueryable();

        return await query.ToListAsync();
    }

    [HttpGet("center")]
    public async Task<List<CenterEntity>> GetAllCenters()
    {
        var query = _db.Centers.AsQueryable();

        return await query.ToListAsync();
    }

    [HttpGet("cross")]
    public async Task<List<CrossEntity>> GetAllCross()
    {
        var query = _db.Crosses.AsQueryable();

        return await query.ToListAsync();
    }

    // Update content only (not property/value)
    [HttpPut("attributes/{id}")]
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