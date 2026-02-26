using HumanDesign.Domain.Models.Diagram;
using HumanDesign.Application.Interfaces;
using System.Text;

namespace HumanDesign.Application.Services.Diagram;

public class SvgDiagramRenderer : IDiagramRenderer
{
    public Task<string> RenderSvgAsync(HumanDesignDiagramModel model)
    {
        var sb = new StringBuilder();

        sb.AppendLine("<svg width='500' height='800' xmlns='http://www.w3.org/2000/svg'>");

        // Draw centers
        int y = 50;

        foreach (var center in model.DefinedCenters)
        {
            sb.AppendLine($"""
                <rect x="200" y="{y}" width="100" height="40"
                      fill="black" stroke="white"/>
                <text x="250" y="{y + 25}" fill="white"
                      font-size="12" text-anchor="middle">
                      {center}
                </text>
            """);

            y += 60;
        }

        // Draw gate indicators
        int gx = 20;
        foreach (var gate in model.ActiveGates)
        {
            sb.AppendLine($"""
                <circle cx="{gx}" cy="750" r="8" fill="red"/>
                <text x="{gx}" y="770" font-size="8"
                      text-anchor="middle">{gate}</text>
            """);

            gx += 20;
        }

        sb.AppendLine("</svg>");

        return Task.FromResult(sb.ToString());
    }
}