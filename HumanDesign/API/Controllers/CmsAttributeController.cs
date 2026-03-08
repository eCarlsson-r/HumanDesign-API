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
    public async Task<AttributeEntity?> Get(int id) => await _db.Attributes.FindAsync(id);

    [HttpGet("attributes")]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = _db.Attributes.AsQueryable();

        var total = await query.CountAsync();

        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return Ok(new { total, items });
    }

    [HttpGet("type/{id}")]
    public async Task<TypeEntity?> GetType(int id) => await _db.Types.FindAsync(id);

    [HttpGet("type")]
    public async Task<IActionResult> GetAllTypes(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = _db.Types.AsQueryable();

        var total = await query.CountAsync();

        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return Ok(new { total, items });
    }
    
    [HttpGet("profile/{id}")]
    public async Task<ProfileEntity?> GetProfile(int id) => await _db.Profiles.FindAsync(id);
    
    [HttpGet("profile")]
    public async Task<IActionResult> GetAllProfiles(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = _db.Profiles.AsQueryable();

        var total = await query.CountAsync();

        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return Ok(new { total, items });
    }

    [HttpGet("gate/{id}")]
    public async Task<GateEntity?> GetGate(int id) => await _db.Gates.FindAsync(id);

    [HttpGet("gate")]
    public async Task<IActionResult> GetAllGates(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = _db.Gates.AsQueryable();

        var total = await query.CountAsync();

        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return Ok(new { total, items });
    }

    [HttpGet("channel/{id}")]
    public async Task<ChannelEntity?> GetChannel(int id) => await _db.Channels.FindAsync(id);

    [HttpGet("channel")]
    public async Task<IActionResult> GetAllChannels(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = _db.Channels.AsQueryable();

        var total = await query.CountAsync();

        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return Ok(new { total, items });
    }

    [HttpGet("center/{id}")]
    public async Task<CenterEntity?> GetCenter(int id) => await _db.Centers.FindAsync(id);

    [HttpGet("center")]
    public async Task<IActionResult> GetAllCenters(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = _db.Centers.AsQueryable();

        var total = await query.CountAsync();

        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return Ok(new { total, items });
    }

    [HttpGet("cross/{id}")]
    public async Task<CrossEntity?> GetCross(int id) => await _db.Crosses.FindAsync(id);

    [HttpGet("cross")]
    public async Task<IActionResult> GetAllCross(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = _db.Crosses.AsQueryable();

        var total = await query.CountAsync();

        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return Ok(new { total, items });
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

    // Update content only (not property/value)
    [HttpPut("type/{id}")]
    public async Task<IActionResult> UpdateType(int id, [FromBody] TypeEntity dto)
    {
        var entity = await _db.Types.FindAsync(id);
        if (entity == null)
            return NotFound();

        entity.Preview = dto.Preview;
        entity.Summary = dto.Summary;
        entity.Detail = dto.Detail;

        await _db.SaveChangesAsync();

        return Ok();
    }

    // Update content only (not property/value)
    [HttpPut("profile/{id}")]
    public async Task<IActionResult> UpdateProfile(int id, [FromBody] ProfileEntity dto)
    {
        var entity = await _db.Profiles.FindAsync(id);
        if (entity == null)
            return NotFound();

        entity.Preview = dto.Preview;
        entity.Summary = dto.Summary;
        entity.Detail = dto.Detail;

        await _db.SaveChangesAsync();

        return Ok();
    }

    // Update content only (not property/value)
    [HttpPut("center/{id}")]
    public async Task<IActionResult> UpdateCenter(int id, [FromBody] CenterEntity dto)
    {
        var entity = await _db.Centers.FindAsync(id);
        if (entity == null)
            return NotFound();

        entity.Preview = dto.Preview;
        entity.Summary = dto.Summary;
        entity.Detail = dto.Detail;

        await _db.SaveChangesAsync();

        return Ok();
    }

    // Update content only (not property/value)
    [HttpPut("gate/{id}")]
    public async Task<IActionResult> UpdateGate(int id, [FromBody] GateEntity dto)
    {
        var entity = await _db.Gates.FindAsync(id);
        if (entity == null)
            return NotFound();

        entity.Title = dto.Title;
        entity.Preview = dto.Preview;
        entity.Summary = dto.Summary;
        entity.Detail = dto.Detail;

        await _db.SaveChangesAsync();

        return Ok();
    }

    // Update content only (not property/value)
    [HttpPut("channel/{id}")]
    public async Task<IActionResult> UpdateChannel(int id, [FromBody] ChannelEntity dto)
    {
        var entity = await _db.Channels.FindAsync(id);
        if (entity == null)
            return NotFound();

        entity.Preview = dto.Preview;
        entity.Summary = dto.Summary;
        entity.Detail = dto.Detail;

        await _db.SaveChangesAsync();

        return Ok();
    }

    // Update content only (not property/value)
    [HttpPut("cross/{id}")]
    public async Task<IActionResult> UpdateCross(int id, [FromBody] CrossEntity dto)
    {
        var entity = await _db.Crosses.FindAsync(id);
        if (entity == null)
            return NotFound();

        entity.Preview = dto.Preview;
        entity.Summary = dto.Summary;
        entity.Detail = dto.Detail;

        await _db.SaveChangesAsync();

        return Ok();
    }
}