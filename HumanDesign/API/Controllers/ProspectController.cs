using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HumanDesign.Application.Interfaces;
using HumanDesign.Domain.Models.Requests;
using HumanDesign.Infrastructure.Data;

namespace HumanDesign.API.Controllers;
[ApiController]
[Route("api/prospects")]
public class ProspectController(AppDbContext db, IProspectService service, IHumanDesignReportBuilder builder) : ControllerBase
{
    private readonly AppDbContext _db = db;
    private readonly IProspectService _service = service;
    private readonly IHumanDesignReportBuilder _builder = builder;

    [HttpPost]
    public async Task<IActionResult> Create(CreateProspectRequest request)
    {
        var id = await _service.CreateProspectAsync(request);
        return Ok(id);
    }

    [HttpGet("{id}/report")]
    public async Task<IActionResult> GetReport(Guid id)
    {
        var designId = await _db.Designs.Where(d => d.ProspectId == id).Select(p => p.Id).FirstOrDefaultAsync();
        var report = await _builder.BuildPreviewAsync(designId);
        return Ok(report);
    }
}