using Microsoft.AspNetCore.Mvc;
using HumanDesign.Application.Interfaces;
using HumanDesign.Application.Services.Diagram;

namespace HumanDesign.API.Controllers;

[ApiController]
[Route("api/diagram")]
public class DiagramController : ControllerBase
{
    private readonly DiagramModelBuilder _builder;
    private readonly IDiagramRenderer _renderer;

    public DiagramController(
        DiagramModelBuilder builder,
        IDiagramRenderer renderer)
    {
        _builder = builder;
        _renderer = renderer;
    }

    [HttpGet("{designId}")]
    public async Task<IActionResult> GetSvg(Guid designId)
    {
        var model = await _builder.BuildAsync(designId);
        var svg = await _renderer.RenderSvgAsync(model);

        return Content(svg, "image/svg+xml");
    }
}