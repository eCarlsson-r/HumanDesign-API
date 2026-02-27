using System.Xml.Linq;
using HumanDesign.Application.Interfaces;
using HumanDesign.Domain.Models.Diagram;
using HumanDesign.Infrastructure.Entities.Charts;

namespace HumanDesign.Application.Services.Diagram;
public class SvgDiagramRenderer(IWebHostEnvironment env) : IDiagramRenderer
{
    private readonly IWebHostEnvironment _env = env;

    public async Task<string> RenderSvgAsync(HumanDesignDiagramModel model)
    {
        var path = Path.Combine(_env.ContentRootPath, "Application/Assets", "bodygraph-template.svg");

        var svgText = await File.ReadAllTextAsync(path);
        var doc = XDocument.Parse(svgText);

        ColorCenters(doc, model.DefinedCenters);
        ColorGates(doc, model.ActiveGates);
        RenderArrows(doc, model.VariableArrows);

        return doc.ToString();
    }

    private static void ColorCenters(XDocument doc, List<CenterDefinition> centers)
    {
        foreach (var center in centers)
        {
            var element = doc.Descendants()
                .FirstOrDefault(x => x.Attribute("id")?.ToString() == $"center-{center.CenterName}");

            if (element == null) continue;

            string color;

            if (center.Definition == "defined")
            {
                color = CenterColorMap.DefinedColors.TryGetValue(center.CenterName, out var c)
                    ? c
                    : "#CCCCCC"; // fallback
            }
            else
            {
                color = CenterColorMap.UndefinedColor;
            }

            element.SetAttributeValue("fill", color);
            element.SetAttributeValue("stroke", "#333");
            element.SetAttributeValue("stroke-width", "2");
        }
    }

    private static void ColorGates(XDocument doc, List<PlanetaryActivation> activations)
    {
        foreach (var activation in activations)
        {
            var id = $"{activation.Type}-{activation.Gate}";

            var channelElement = doc.Descendants()
                .FirstOrDefault(x => x.Attribute("id")?.ToString() == id);

            channelElement?.SetAttributeValue("fill", activation.Type == "design" ? "#FF0000" : "#000000");

            var gateElement = doc.Descendants()
                .FirstOrDefault(x => x.Attribute("id")?.ToString() == $"{activation.Gate}");

            gateElement?.SetAttributeValue("fill", "#000000");
        }

        var distinctGate = activations.GroupBy(p => p.Gate).Where(group => group.Count() == 1);
        foreach (var group in distinctGate)
        {
            foreach (var activation in group)
            {
                var id = $"{activation.Type}-{activation.Gate}";

                var designElement = doc.Descendants()
                    .FirstOrDefault(x => x.Attribute("id")?.ToString() == id);

                designElement?.SetAttributeValue("fill", activation.Type == "design" ? "#FF0000" : "#000000");

                var personalityElement = doc.Descendants()
                    .FirstOrDefault(x => x.Attribute("id")?.ToString() == id);

                personalityElement?.SetAttributeValue("fill", activation.Type == "design" ? "#FF0000" : "#000000");
            }
        }
    }

    private static void RenderArrows(XDocument doc, VariableSet variables)
    {
        SetArrow(doc, "digestion-arrow", variables.DigestionArrow.IsLeft);
        SetText(doc, "digestion-color", variables.DigestionArrow.Color);
        SetText(doc, "digestion-tone", variables.DigestionArrow.Tone);
        
        SetArrow(doc, "environment-arrow", variables.EnvironmentArrow.IsLeft);
        SetText(doc, "environment-color", variables.EnvironmentArrow.Color);
        SetText(doc, "environment-tone", variables.EnvironmentArrow.Tone);

        SetArrow(doc, "awareness-arrow", variables.AwarenessArrow.IsLeft);
        SetText(doc, "awareness-color", variables.AwarenessArrow.Color);
        SetText(doc, "awareness-tone", variables.AwarenessArrow.Tone);

        SetArrow(doc, "perspective-arrow", variables.PerspectiveArrow.IsLeft);
        SetText(doc, "perspective-color", variables.PerspectiveArrow.Color);
        SetText(doc, "perspective-tone", variables.PerspectiveArrow.Tone);
    }

    private static void SetArrow(XDocument doc, string id, bool isLeft)
    {
        var element = doc.Descendants()
            .FirstOrDefault(x => x.Attribute("id")?.ToString() == id);

        if (element == null) return;

        var baseTransform = element.Attribute("transform")?.Value ?? "";

        if (!isLeft)
        {
            element.SetAttributeValue(
                "transform",
                baseTransform + " rotate(180deg)"
            );
        }
    }

    private static void SetText(XDocument doc, string id, int number)
    {
        var element = doc.Descendants()
            .FirstOrDefault(x => x.Attribute("id")?.ToString() == id);

        if (element == null) return;

        element.SetValue(number);
    }
}