using HumanDesign.Domain.Models.Diagram;

namespace HumanDesign.Application.Interfaces;

public interface IDiagramRenderer
{
    Task<string> RenderSvgAsync(HumanDesignDiagramModel model);
}