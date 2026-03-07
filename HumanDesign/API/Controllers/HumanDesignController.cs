using Microsoft.AspNetCore.Mvc;
using HumanDesign.Application.Interfaces;

namespace HumanDesign.API.Controllers;

[ApiController]
[Route("api/human-design")]
public class HumanDesignController(IHumanDesignReportBuilder builder) : ControllerBase
{
    private readonly IHumanDesignReportBuilder _builder = builder;

    [HttpGet("{id}/preview")]
    public async Task<IActionResult> Preview(Guid id)
    {
        var report = await _builder.BuildPreviewAsync(id);
        return Ok(report);
    }

    [HttpGet("{id}/summary")]
    public async Task<IActionResult> Summary(Guid id)
    {
        var report = await _builder.BuildSummaryAsync(id);
        return Ok(report);
    }

    [HttpGet("{id}/detail")]
    public async Task<IActionResult> Detail(Guid id)
    {
        var report = await _builder.BuildDetailAsync(id);
        return Ok(report);
    }
}