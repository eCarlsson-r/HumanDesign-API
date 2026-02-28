using Microsoft.AspNetCore.Mvc;
using HumanDesign.Application.Interfaces;
using HumanDesign.Domain.Models.Requests;

namespace HumanDesign.API.Controllers;
[ApiController]
[Route("api/prospects")]
public class ProspectController : ControllerBase
{
    private readonly IProspectService _service;

    public ProspectController(IProspectService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProspectRequest request)
    {
        var id = await _service.CreateProspectAsync(request);
        return Ok(id);
    }

    [HttpGet("{id}/report")]
    public async Task<IActionResult> GetReport(Guid id)
    {
        var report = await _service.GetReportAsync(id);
        return Ok(report);
    }
}