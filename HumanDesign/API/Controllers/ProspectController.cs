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
    public async Task<IActionResult> Create(CreateProspectRequest req)
    {
        var id = await _service.CreateProspectAsync(req);

        return Ok(new
        {
            ProspectId = id,
            Message = "Chart is being generated"
        });
    }
}