using HumanDesignApi.Models;
using System.Text;

namespace HumanDesignApi.Services;

public class SvgService
{
    private const string PERSONAL_COLOR = "#495057"; // Gray
    private const string DESIGN_COLOR = "#d9534f"; // Red

    public string GenerateDesignImage(LegacyHumanDesignResponse testData)
    {
        var gates = new string[65];
        for (int i = 0; i < 65; i++) gates[i] = "none";

        var unconscious = new List<int>();
        var conscious = new List<int>();

        void ExtractGates(string? val, List<int> list)
        {
            if (string.IsNullOrEmpty(val)) return;
            var parts = val.Split('.');
            if (parts.Length > 0 && int.TryParse(parts[0], out int gate)) list.Add(gate);
        }

        ExtractGates(testData.DesignUc01, unconscious);
        ExtractGates(testData.DesignUc02, unconscious);
        ExtractGates(testData.DesignUc03, unconscious);
        ExtractGates(testData.DesignUc04, unconscious);
        ExtractGates(testData.DesignUc05, unconscious);
        ExtractGates(testData.DesignUc06, unconscious);
        ExtractGates(testData.DesignUc07, unconscious);
        ExtractGates(testData.DesignUc08, unconscious);
        ExtractGates(testData.DesignUc09, unconscious);
        ExtractGates(testData.DesignUc10, unconscious);
        ExtractGates(testData.DesignUc11, unconscious);
        ExtractGates(testData.DesignUc12, unconscious);
        ExtractGates(testData.DesignUc13, unconscious);
        ExtractGates(testData.DesignUc14, unconscious);
        ExtractGates(testData.DesignUc15, unconscious);

        ExtractGates(testData.DesignC01, conscious);
        ExtractGates(testData.DesignC02, conscious);
        ExtractGates(testData.DesignC03, conscious);
        ExtractGates(testData.DesignC04, conscious);
        ExtractGates(testData.DesignC05, conscious);
        ExtractGates(testData.DesignC06, conscious);
        ExtractGates(testData.DesignC07, conscious);
        ExtractGates(testData.DesignC08, conscious);
        ExtractGates(testData.DesignC09, conscious);
        ExtractGates(testData.DesignC10, conscious);
        ExtractGates(testData.DesignC11, conscious);
        ExtractGates(testData.DesignC12, conscious);
        ExtractGates(testData.DesignC13, conscious);
        ExtractGates(testData.DesignC14, conscious);
        ExtractGates(testData.DesignC15, conscious);

        var allActivatedGates = unconscious.Concat(conscious).Distinct();
        foreach (var gate in allActivatedGates)
        {
            bool isUnconscious = unconscious.Contains(gate);
            bool isConscious = conscious.Contains(gate);
            if (isUnconscious && isConscious) gates[gate] = "both";
            else if (isUnconscious) gates[gate] = "design";
            else if (isConscious) gates[gate] = "personality";
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("<svg width=\"490\" height=\"707\" viewBox=\"0 0 490 707\" xmlns=\"http://www.w3.org/2000/svg\" class=\"hdapi-svg-style hdapi-line-style-fill\">");
        sb.Append("<g stroke=\"none\" stroke-width=\"1\" fill=\"none\" fill-rule=\"evenodd\">");
        sb.Append($"<path d=\"{SvgPaths.BackgroundPath}\" fill=\"#f2f2f2\"></path>");
        
        string GetFill(int gate, string target)
        {
            if (gates[gate] == "both") return target == "personality" ? PERSONAL_COLOR : DESIGN_COLOR;
            if (gates[gate] == target) return target == "personality" ? PERSONAL_COLOR : DESIGN_COLOR;
            return "";
        }

        // 39 & 55
        sb.Append("<g transform=\"translate(272.000000, 566.000000)\">");
        sb.Append($"<path id=\"personality-39\" fill=\"{GetFill(39, "personality")}\" d=\"{SvgPaths.Personality39}\"></path>");
        sb.Append($"<path id=\"design-39\" fill=\"{GetFill(39, "design")}\" d=\"{SvgPaths.Design39}\"></path>");
        sb.Append($"<path id=\"personality-55\" fill=\"{GetFill(55, "personality")}\" d=\"{SvgPaths.Personality55}\"></path>");
        sb.Append($"<path id=\"design-55\" fill=\"{GetFill(55, "design")}\" d=\"{SvgPaths.Design55}\"></path>");
        sb.Append("</g>");

        // 30 & 41
        sb.Append("<g transform=\"translate(285.000000, 582.000000)\">");
        sb.Append($"<path id=\"personality-30\" fill=\"{GetFill(30, "personality")}\" d=\"{SvgPaths.Personality30}\"></path>");
        sb.Append($"<path id=\"design-30\" fill=\"{GetFill(30, "design")}\" d=\"{SvgPaths.Design30}\"></path>");
        sb.Append($"<path id=\"personality-41\" fill=\"{GetFill(41, "personality")}\" d=\"{SvgPaths.Personality41}\"></path>");
        sb.Append($"<path id=\"design-41\" fill=\"{GetFill(41, "design")}\" d=\"{SvgPaths.Design41}\"></path>");
        sb.Append("</g>");

        // 49 & 19
        sb.Append("<g transform=\"translate(267.000000, 545.000000)\">");
        sb.Append($"<path id=\"personality-49\" fill=\"{GetFill(49, "personality")}\" d=\"{SvgPaths.Personality49}\"></path>");
        sb.Append($"<path id=\"design-49\" fill=\"{GetFill(49, "design")}\" d=\"{SvgPaths.Design49}\"></path>");
        sb.Append($"<path id=\"personality-19\" fill=\"{GetFill(19, "personality")}\" d=\"{SvgPaths.Personality19}\"></path>");
        sb.Append($"<path id=\"design-19\" fill=\"{GetFill(19, "design")}\" d=\"{SvgPaths.Design19}\"></path>");
        sb.Append("</g>");

        // 18 & 58 (flipped)
        sb.Append("<g transform=\"translate(59.000000, 583.000000)\">");
        sb.Append($"<path id=\"personality-18\" fill=\"{GetFill(18, "personality")}\" d=\"{SvgPaths.Personality18}\" transform=\"translate(28.000000, 37.000000) scale(-1, 1) translate(-28.000000, -37.000000)\"></path>");
        sb.Append($"<path id=\"design-18\" fill=\"{GetFill(18, "design")}\" d=\"{SvgPaths.Design18}\" transform=\"translate(31.000000, 32.500000) scale(-1, 1) translate(-31.000000, -32.500000)\"></path>");
        sb.Append($"<path id=\"personality-58\" fill=\"{GetFill(58, "personality")}\" d=\"{SvgPaths.Personality58}\" transform=\"translate(99.999779, 83.000000) scale(-1, 1) translate(-99.999779, -83.000000)\"></path>");
        sb.Append($"<path id=\"design-58\" fill=\"{GetFill(58, "design")}\" d=\"{SvgPaths.Design58}\" transform=\"translate(100.500000, 79.500000) scale(-1, 1) translate(-100.500000, -79.500000)\"></path>");
        sb.Append("</g>");

        // 38 & 28 (flipped)
        sb.Append("<g transform=\"translate(85.000000, 563.000000)\">");
        sb.Append($"<path id=\"personality-38\" fill=\"{GetFill(38, "personality")}\" d=\"{SvgPaths.Personality38}\" transform=\"translate(25.359760, 39.645268) scale(-1, 1) translate(-25.359760, -39.645268)\"></path>");
        sb.Append($"<path id=\"design-38\" fill=\"{GetFill(38, "design")}\" d=\"{SvgPaths.Design38}\" transform=\"translate(28.549220, 37.231201) scale(-1, 1) translate(-28.549220, -37.231201)\"></path>");
        sb.Append($"<path id=\"personality-28\" fill=\"{GetFill(28, "personality")}\" d=\"{SvgPaths.Personality28}\" transform=\"translate(11.154078, 19.615489) scale(-1, 1) translate(-11.154078, -19.615489)\"></path>");
        sb.Append($"<path id=\"design-28\" fill=\"{GetFill(28, "design")}\" d=\"{SvgPaths.Design28}\" transform=\"translate(12.939872, 15.622140) scale(-1, 1) translate(-12.939872, -15.622140)\"></path>");
        sb.Append("</g>");

        // 32 & 54 (flipped)
        sb.Append("<g transform=\"translate(133.000000, 545.000000)\">");
        sb.Append($"<path id=\"personality-32\" fill=\"{GetFill(32, "personality")}\" d=\"{SvgPaths.Personality32}\" transform=\"translate(17.915220, 15.649231) scale(-1, 1) translate(-17.915220, -15.649231)\"></path>");
        sb.Append($"<path id=\"design-32\" fill=\"{GetFill(32, "design")}\" d=\"{SvgPaths.Design32}\" transform=\"translate(19.242012, 12.248553) scale(-1, 1) translate(-19.242012, -12.248553)\"></path>");
        sb.Append($"<path id=\"design-54\" fill=\"{GetFill(54, "design")}\" d=\"{SvgPaths.Design54}\" transform=\"translate(42.376097, 29.713892) scale(-1, 1) translate(-42.376097, -29.713892)\"></path>");
        sb.Append($"<path id=\"personality-54\" fill=\"{GetFill(54, "personality")}\" d=\"{SvgPaths.Personality54}\" transform=\"translate(39.547583, 32.386316) scale(-1, 1) translate(-39.547583, -32.386316)\"></path>");
        sb.Append("</g>");

        // 27 & 50
        sb.Append("<g transform=\"translate(158.000000, 529.000000)\">");
        sb.Append($"<path id=\"design-27\" fill=\"{GetFill(27, "design")}\" d=\"{SvgPaths.Design27}\"></path>");
        sb.Append($"<path id=\"personality-27\" fill=\"{GetFill(27, "personality")}\" d=\"{SvgPaths.Personality27}\"></path>");
        sb.Append($"<path id=\"design-50\" fill=\"{GetFill(50, "design")}\" d=\"{SvgPaths.Design50}\"></path>");
        sb.Append($"<path id=\"personality-50\" fill=\"{GetFill(50, "personality")}\" d=\"{SvgPaths.Personality50}\"></path>");
        sb.Append("</g>");

        // Rect gates (Bars)
        // Group 9, 52 (261, 557)
        sb.Append("<g transform=\"translate(261.000000, 557.000000)\">");
        sb.Append($"<path id=\"personality-9\" fill=\"{GetFill(9, "personality")}\" d=\"{SvgPaths.RectGate9}\"></path>");
        sb.Append($"<path id=\"design-9\" fill=\"{GetFill(9, "design")}\" d=\"{SvgPaths.RectGate9}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"personality-52\" fill=\"{GetFill(52, "personality")}\" d=\"{SvgPaths.RectGate52}\"></path>");
        sb.Append($"<path id=\"design-52\" fill=\"{GetFill(52, "design")}\" d=\"{SvgPaths.RectGate52}\" transform=\"translate(5, 0)\"></path>");
        sb.Append("</g>");

        // Group 3, 60 (241, 568)
        sb.Append("<g transform=\"translate(241.000000, 568.000000)\">");
        sb.Append($"<path id=\"personality-3\" fill=\"{GetFill(3, "personality")}\" d=\"{SvgPaths.RectGate3}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-3\" fill=\"{GetFill(3, "design")}\" d=\"{SvgPaths.RectGate3}\"></path>");
        sb.Append($"<path id=\"personality-60\" fill=\"{GetFill(60, "personality")}\" d=\"{SvgPaths.RectGate60}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-60\" fill=\"{GetFill(60, "design")}\" d=\"{SvgPaths.RectGate60}\"></path>");
        sb.Append("</g>");

        // Group 42, 53 (221, 557)
        sb.Append("<g transform=\"translate(221.000000, 557.000000)\">");
        sb.Append($"<path id=\"personality-42\" fill=\"{GetFill(42, "personality")}\" d=\"{SvgPaths.RectGate42}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-42\" fill=\"{GetFill(42, "design")}\" d=\"{SvgPaths.RectGate42}\"></path>");
        sb.Append($"<path id=\"personality-53\" fill=\"{GetFill(53, "personality")}\" d=\"{SvgPaths.RectGate53}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-53\" fill=\"{GetFill(53, "design")}\" d=\"{SvgPaths.RectGate53}\"></path>");
        sb.Append("</g>");

        // Group 29, 46 (261, 427)
        sb.Append("<g transform=\"translate(261.000000, 427.000000)\">");
        sb.Append($"<path id=\"personality-29\" fill=\"{GetFill(29, "personality")}\" d=\"{SvgPaths.RectGate29}\"></path>");
        sb.Append($"<path id=\"design-29\" fill=\"{GetFill(29, "design")}\" d=\"{SvgPaths.RectGate29}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"personality-46\" fill=\"{GetFill(46, "personality")}\" d=\"{SvgPaths.RectGate46}\"></path>");
        sb.Append($"<path id=\"design-46\" fill=\"{GetFill(46, "design")}\" d=\"{SvgPaths.RectGate46}\" transform=\"translate(5, 0)\"></path>");
        sb.Append("</g>");

        // Group 5, 15 (221, 427)
        sb.Append("<g transform=\"translate(221.000000, 427.000000)\">");
        sb.Append($"<path id=\"personality-5\" fill=\"{GetFill(5, "personality")}\" d=\"{SvgPaths.RectGate5}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-5\" fill=\"{GetFill(5, "design")}\" d=\"{SvgPaths.RectGate5}\"></path>");
        sb.Append($"<path id=\"personality-15\" fill=\"{GetFill(15, "personality")}\" d=\"{SvgPaths.RectGate15}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-15\" fill=\"{GetFill(15, "design")}\" d=\"{SvgPaths.RectGate15}\"></path>");
        sb.Append("</g>");

        // Group 14, 2 (241, 438)
        sb.Append("<g transform=\"translate(241.000000, 438.000000)\">");
        sb.Append($"<path id=\"personality-14\" fill=\"{GetFill(14, "personality")}\" d=\"{SvgPaths.RectGate14}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-14\" fill=\"{GetFill(14, "design")}\" d=\"{SvgPaths.RectGate14}\"></path>");
        sb.Append($"<path id=\"personality-2\" fill=\"{GetFill(2, "personality")}\" d=\"{SvgPaths.RectGate2}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-2\" fill=\"{GetFill(2, "design")}\" d=\"{SvgPaths.RectGate2}\"></path>");
        sb.Append("</g>");

        sb.Append("<g transform=\"translate(51.000000, 80.000000)\">");
        sb.Append($"<path id=\"personality-57\" fill=\"{GetFill(57, "personality")}\" d=\"{SvgPaths.Personality57}\"></path>");
        sb.Append($"<path id=\"design-57\" fill=\"{GetFill(57, "design")}\" d=\"{SvgPaths.Design57}\"></path>");
        sb.Append($"<path id=\"personality-34\" fill=\"{GetFill(34, "personality")}\" d=\"{SvgPaths.Personality34}\"></path>");
        sb.Append($"<path id=\"design-34\" fill=\"{GetFill(34, "design")}\" d=\"{SvgPaths.Design34}\"></path>");
        sb.Append($"<path id=\"personality-10\" fill=\"{GetFill(10, "personality")}\" d=\"{SvgPaths.Personality10}\"></path>");
        sb.Append($"<path id=\"design-10\" fill=\"{GetFill(10, "design")}\" d=\"{SvgPaths.Design10}\"></path>");
        sb.Append($"<path id=\"personality-20\" fill=\"{GetFill(20, "personality")}\" d=\"{SvgPaths.Personality20}\"></path>");
        sb.Append($"<path id=\"design-20\" fill=\"{GetFill(20, "design")}\" d=\"{SvgPaths.Design20}\"></path>");
        sb.Append($"<path id=\"personality-59\" fill=\"{GetFill(59, "personality")}\" d=\"{SvgPaths.Personality59}\"></path>");
        sb.Append($"<path id=\"design-59\" fill=\"{GetFill(59, "design")}\" d=\"{SvgPaths.Design59}\"></path>");
        sb.Append($"<path id=\"personality-6\" fill=\"{GetFill(6, "personality")}\" d=\"{SvgPaths.Personality6}\"></path>");
        sb.Append($"<path id=\"design-6\" fill=\"{GetFill(6, "design")}\" d=\"{SvgPaths.Design6}\"></path>");
        sb.Append($"<path id=\"personality-26\" fill=\"{GetFill(26, "personality")}\" d=\"{SvgPaths.Personality26}\"></path>");
        sb.Append($"<path id=\"design-26\" fill=\"{GetFill(26, "design")}\" d=\"{SvgPaths.Design26}\"></path>");
        sb.Append($"<path id=\"personality-44\" fill=\"{GetFill(44, "personality")}\" d=\"{SvgPaths.Personality44}\"></path>");
        sb.Append($"<path id=\"design-44\" fill=\"{GetFill(44, "design")}\" d=\"{SvgPaths.Design44}\"></path>");
        sb.Append("</g>");

        // 37, 40, 25, 51 (Complex transforms)
        sb.Append("<g transform=\"translate(364.000000, 470.000000)\">");
        sb.Append($"<path id=\"personality-37\" fill=\"{GetFill(37, "personality")}\" d=\"{SvgPaths.Personality37}\" transform=\"translate(25.359760, 39.645268) scale(-1, 1) rotate(-57.000000) translate(-25.359760, -39.645268)\"></path>");
        sb.Append($"<path id=\"design-37\" fill=\"{GetFill(37, "design")}\" d=\"{SvgPaths.Design37}\" transform=\"translate(28.549220, 37.231201) scale(-1, 1) rotate(-57.000000) translate(-28.549220, -37.231201)\"></path>");
        sb.Append($"<path id=\"personality-40\" fill=\"{GetFill(40, "personality")}\" d=\"{SvgPaths.Personality40}\" transform=\"translate(11.154078, 19.615489) scale(-1, 1) rotate(-57.000000) translate(-11.154078, -19.615489)\"></path>");
        sb.Append($"<path id=\"design-40\" fill=\"{GetFill(40, "design")}\" d=\"{SvgPaths.Design40}\" transform=\"translate(12.939872, 15.622140) scale(-1, 1) rotate(-57.000000) translate(12.939872, -15.622140)\"></path>");
        sb.Append("</g>");

        sb.Append("<g transform=\"translate(283.000000, 395.000000)\">");
        sb.Append($"<path id=\"personality-25\" fill=\"{GetFill(25, "personality")}\" d=\"{SvgPaths.Personality25}\" transform=\"translate(18.231382, 15.814831) rotate(4.000000) translate(-18.231382, -15.814831)\"></path>");
        sb.Append($"<path id=\"design-25\" fill=\"{GetFill(25, "design")}\" d=\"{SvgPaths.Design25}\" transform=\"translate(18.242012, 10.248553) rotate(4.000000) translate(-18.242012, -10.248553)\"></path>");
        sb.Append($"<path id=\"design-51\" fill=\"{GetFill(51, "design")}\" d=\"{SvgPaths.Design51}\" transform=\"translate(42.376097, 29.713892) rotate(4.000000) translate(-42.376097, -29.713892)\"></path>");
        sb.Append($"<path id=\"personality-51\" fill=\"{GetFill(51, "personality")}\" d=\"{SvgPaths.Personality51}\" transform=\"translate(39.547583, 32.386316) rotate(4.000000) translate(-39.547583, -32.386316)\"></path>");
        sb.Append("</g>");

        // 21 & 45
        sb.Append("<g transform=\"translate(233.000000, 237.000000)\">");
        sb.Append($"<path id=\"personality-21\" fill=\"{GetFill(21, "personality")}\" d=\"{SvgPaths.Personality21}\"></path>");
        sb.Append($"<path id=\"design-21\" fill=\"{GetFill(21, "design")}\" d=\"{SvgPaths.Design21}\"></path>");
        sb.Append($"<path id=\"personality-45\" fill=\"{GetFill(45, "personality")}\" d=\"{SvgPaths.Personality45}\"></path>");
        sb.Append($"<path id=\"design-45\" fill=\"{GetFill(45, "design")}\" d=\"{SvgPaths.Design45}\"></path>");
        sb.Append("</g>");

        // 22 & 12
        sb.Append("<g transform=\"translate(259.000000, 314.000000)\">");
        sb.Append($"<path id=\"personality-22\" fill=\"{GetFill(22, "personality")}\" d=\"{SvgPaths.Personality22}\"></path>");
        sb.Append($"<path id=\"design-22\" fill=\"{GetFill(22, "design")}\" d=\"{SvgPaths.Design22}\"></path>");
        sb.Append($"<path id=\"personality-12\" fill=\"{GetFill(12, "personality")}\" d=\"{SvgPaths.Personality12}\"></path>");
        sb.Append($"<path id=\"design-12\" fill=\"{GetFill(12, "design")}\" d=\"{SvgPaths.Design12}\"></path>");
        sb.Append("</g>");

        // 36 & 35
        sb.Append("<g transform=\"translate(259.000000, 240.000000)\">");
        sb.Append($"<path id=\"personality-36\" fill=\"{GetFill(36, "personality")}\" d=\"{SvgPaths.Personality36}\"></path>");
        sb.Append($"<path id=\"design-36\" fill=\"{GetFill(36, "design")}\" d=\"{SvgPaths.Design36}\"></path>");
        sb.Append($"<path id=\"personality-35\" fill=\"{GetFill(35, "personality")}\" d=\"{SvgPaths.Personality35}\"></path>");
        sb.Append($"<path id=\"design-35\" fill=\"{GetFill(35, "design")}\" d=\"{SvgPaths.Design35}\"></path>");
        sb.Append("</g>");

        // 48 & 16
        sb.Append("<g transform=\"translate(133.000000, 240.000000)\">");
        sb.Append($"<path id=\"personality-48\" fill=\"{GetFill(48, "personality")}\" d=\"{SvgPaths.Personality48}\"></path>");
        sb.Append($"<path id=\"design-48\" fill=\"{GetFill(48, "design")}\" d=\"{SvgPaths.Design48}\"></path>");
        sb.Append($"<path id=\"personality-16\" fill=\"{GetFill(16, "personality")}\" d=\"{SvgPaths.Personality16}\"></path>");
        sb.Append($"<path id=\"design-16\" fill=\"{GetFill(16, "design")}\" d=\"{SvgPaths.Design16}\"></path>");
        sb.Append("</g>");

        // 13, 33, 8, 1, 31, 7
        // Group 13, 33 (261, 317)
        sb.Append("<g transform=\"translate(261.000000, 317.000000)\">");
        sb.Append($"<path id=\"personality-13\" fill=\"{GetFill(13, "personality")}\" d=\"{SvgPaths.RectGate13}\"></path>");
        sb.Append($"<path id=\"design-13\" fill=\"{GetFill(13, "design")}\" d=\"{SvgPaths.RectGate13}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"personality-33\" fill=\"{GetFill(33, "personality")}\" d=\"{SvgPaths.RectGate33}\"></path>");
        sb.Append($"<path id=\"design-33\" fill=\"{GetFill(33, "design")}\" d=\"{SvgPaths.RectGate33}\" transform=\"translate(5, 0)\"></path>");
        sb.Append("</g>");

        // Group 8, 1 (241, 317)
        sb.Append("<g transform=\"translate(241.000000, 317.000000)\">");
        sb.Append($"<path id=\"personality-8\" fill=\"{GetFill(8, "personality")}\" d=\"{SvgPaths.RectGate8}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-8\" fill=\"{GetFill(8, "design")}\" d=\"{SvgPaths.RectGate8}\"></path>");
        sb.Append($"<path id=\"personality-1\" fill=\"{GetFill(1, "personality")}\" d=\"{SvgPaths.RectGate1}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-1\" fill=\"{GetFill(1, "design")}\" d=\"{SvgPaths.RectGate1}\"></path>");
        sb.Append("</g>");

        // Group 31, 7 (221, 317)
        sb.Append("<g transform=\"translate(221.000000, 317.000000)\">");
        sb.Append($"<path id=\"personality-31\" fill=\"{GetFill(31, "personality")}\" d=\"{SvgPaths.RectGate31}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-31\" fill=\"{GetFill(31, "design")}\" d=\"{SvgPaths.RectGate31}\"></path>");
        sb.Append($"<path id=\"personality-7\" fill=\"{GetFill(7, "personality")}\" d=\"{SvgPaths.RectGate7}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-7\" fill=\"{GetFill(7, "design")}\" d=\"{SvgPaths.RectGate7}\"></path>");
        sb.Append("</g>");

        // 56, 11, 43, 23, 17, 62
        // Group 56, 11 (261, 154)
        sb.Append("<g transform=\"translate(261.000000, 154.000000)\">");
        sb.Append($"<path id=\"personality-56\" fill=\"{GetFill(56, "personality")}\" d=\"{SvgPaths.RectGate56}\"></path>");
        sb.Append($"<path id=\"design-56\" fill=\"{GetFill(56, "design")}\" d=\"{SvgPaths.RectGate56}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"personality-11\" fill=\"{GetFill(11, "personality")}\" d=\"{SvgPaths.RectGate11}\"></path>");
        sb.Append($"<path id=\"design-11\" fill=\"{GetFill(11, "design")}\" d=\"{SvgPaths.RectGate11}\" transform=\"translate(5, 0)\"></path>");
        sb.Append("</g>");

        // Group 43, 23 (241, 154)
        sb.Append("<g transform=\"translate(241.000000, 154.000000)\">");
        sb.Append($"<path id=\"personality-43\" fill=\"{GetFill(43, "personality")}\" d=\"{SvgPaths.RectGate43}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-43\" fill=\"{GetFill(43, "design")}\" d=\"{SvgPaths.RectGate43}\"></path>");
        sb.Append($"<path id=\"personality-23\" fill=\"{GetFill(23, "personality")}\" d=\"{SvgPaths.RectGate23}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-23\" fill=\"{GetFill(23, "design")}\" d=\"{SvgPaths.RectGate23}\"></path>");
        sb.Append("</g>");

        // Group 17, 62 (221, 154)
        sb.Append("<g transform=\"translate(221.000000, 154.000000)\">");
        sb.Append($"<path id=\"personality-17\" fill=\"{GetFill(17, "personality")}\" d=\"{SvgPaths.RectGate17}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-17\" fill=\"{GetFill(17, "design")}\" d=\"{SvgPaths.RectGate17}\"></path>");
        sb.Append($"<path id=\"personality-62\" fill=\"{GetFill(62, "personality")}\" d=\"{SvgPaths.RectGate62}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-62\" fill=\"{GetFill(62, "design")}\" d=\"{SvgPaths.RectGate62}\"></path>");
        sb.Append("</g>");

        // Group 4, 63, 61, 24, 47, 64
        // Group 4, 63 (261, 72)
        sb.Append("<g transform=\"translate(261.000000, 72.000000)\">");
        sb.Append($"<path id=\"personality-4\" fill=\"{GetFill(4, "personality")}\" d=\"{SvgPaths.RectGate4}\"></path>");
        sb.Append($"<path id=\"design-4\" fill=\"{GetFill(4, "design")}\" d=\"{SvgPaths.RectGate4}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"personality-63\" fill=\"{GetFill(63, "personality")}\" d=\"{SvgPaths.RectGate63}\"></path>");
        sb.Append($"<path id=\"design-63\" fill=\"{GetFill(63, "design")}\" d=\"{SvgPaths.RectGate63}\" transform=\"translate(5, 0)\"></path>");
        sb.Append("</g>");

        // Group 61, 24 (241, 41)
        sb.Append("<g transform=\"translate(241.000000, 41.000000)\">");
        sb.Append($"<path id=\"personality-61\" fill=\"{GetFill(61, "personality")}\" d=\"{SvgPaths.RectGate61}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-61\" fill=\"{GetFill(61, "design")}\" d=\"{SvgPaths.RectGate61}\"></path>");
        sb.Append($"<path id=\"personality-24\" fill=\"{GetFill(24, "personality")}\" d=\"{SvgPaths.RectGate24}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-24\" fill=\"{GetFill(24, "design")}\" d=\"{SvgPaths.RectGate24}\"></path>");
        sb.Append("</g>");

        // Group 47, 64 (221, 58)
        sb.Append("<g transform=\"translate(221.000000, 58.000000)\">");
        sb.Append($"<path id=\"personality-47\" fill=\"{GetFill(47, "personality")}\" d=\"{SvgPaths.RectGate47}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-47\" fill=\"{GetFill(47, "design")}\" d=\"{SvgPaths.RectGate47}\"></path>");
        sb.Append($"<path id=\"personality-64\" fill=\"{GetFill(64, "personality")}\" d=\"{SvgPaths.RectGate64}\" transform=\"translate(5, 0)\"></path>");
        sb.Append($"<path id=\"design-64\" fill=\"{GetFill(64, "design")}\" d=\"{SvgPaths.RectGate64}\"></path>");
        sb.Append("</g>");

        // Center Colors (Defined/Active)
        const string HEAD_COLOR = "#8a02b0";
        const string AJNA_COLOR = "#064afb";
        const string THROAT_COLOR = "#03d5f2";
        const string G_COLOR = "#fdf500";
        const string HEART_COLOR = "#79dc04";
        const string SACRAL_COLOR = "#fa7a2d";
        const string PLEXUS_COLOR = "#fdb304";
        const string SPLEEN_COLOR = "#954d02";
        const string ROOT_COLOR = "#ff1a00";

        string GetCenterFill(string status, string activeColor) => status == "AKTIF" ? activeColor : "#FFFFFF";

        // Borders and Connections (Layered under centers)
        sb.Append("<g transform=\"translate(51.000000, 80.000000)\" stroke=\"black\" stroke-width=\"2\">");
        sb.Append($"<path id=\"border-25-52\" d=\"{SvgPaths.Border25_51}\" transform=\"translate(258.709690, 338.509374) rotate(4.000000) translate(-258.709690, -338.509374)\"></path>");
        sb.Append($"<path id=\"border-20-57-10-34\" d=\"{SvgPaths.Border20_57_10_34}\"></path>");
        sb.Append($"<path id=\"border-top-44-26\" d=\"{SvgPaths.BorderTop44_26}\" stroke-linecap=\"square\"></path>");
        sb.Append($"<path id=\"border-44-26-bottom\" d=\"{SvgPaths.BorderBottom44_26}\" stroke-linecap=\"square\"></path>");
        sb.Append("<line x1=\"200.5\" y1=\"379.5\" x2=\"208.5\" y2=\"379.5\" stroke-linecap=\"square\"></line>");
        sb.Append("<line x1=\"181.5\" y1=\"379.5\" x2=\"189.5\" y2=\"379.5\" stroke-linecap=\"square\"></line>");
        sb.Append("<line x1=\"181.5\" y1=\"388.5\" x2=\"189.5\" y2=\"388.5\" stroke-linecap=\"square\"></line>");
        sb.Append("<line x1=\"200.5\" y1=\"388.5\" x2=\"208.5\" y2=\"388.5\" stroke-linecap=\"square\"></line>");
        sb.Append($"<path id=\"border-40-37-outer\" d=\"{SvgPaths.Border40_37_Outer}\" stroke-linecap=\"square\"></path>");
        sb.Append($"<path id=\"border-40-37-inner\" d=\"{SvgPaths.Border40_37_Inner}\" stroke-linecap=\"square\"></path>");
        sb.Append("<rect id=\"border-64-53\" x=\"170\" y=\"0\" width=\"10\" height=\"599\"></rect>");
        sb.Append("<rect id=\"border-61-60\" x=\"190\" y=\"0\" width=\"10\" height=\"599\"></rect>");
        sb.Append("<line x1=\"221.5\" y1=\"388.5\" x2=\"249.5\" y2=\"388.5\" stroke-linecap=\"square\"></line>");
        sb.Append("<line x1=\"221.5\" y1=\"379.5\" x2=\"248.5\" y2=\"379.5\" stroke-linecap=\"square\"></line>");
        sb.Append($"<path id=\"border-34\" d=\"{SvgPaths.Border34}\" stroke-linecap=\"square\"></path>");
        sb.Append($"<path id=\"border-line-2\" d=\"{SvgPaths.BorderLine2}\" stroke-linecap=\"square\"></path>");
        sb.Append("<rect id=\"border-63-52\" x=\"210\" y=\"0\" width=\"10\" height=\"599\"></rect>");
        sb.Append($"<path id=\"border-30-41\" d=\"{SvgPaths.Border30_41}\"></path>");
        sb.Append($"<path id=\"border-55-39\" d=\"{SvgPaths.Border55_39}\"></path>");
        sb.Append($"<path id=\"border-49-19\" d=\"{SvgPaths.Border49_19}\"></path>");
        sb.Append($"<path id=\"border-18-58\" d=\"{SvgPaths.Border18_58}\" transform=\"translate(82.500000, 554.500000) scale(-1, 1) translate(-82.500000, -554.500000)\"></path>");
        sb.Append($"<path id=\"border-28-38\" d=\"{SvgPaths.Border28_38}\" transform=\"translate(91.500000, 538.000000) scale(-1, 1) translate(-91.500000, -538.000000)\"></path>");
        sb.Append($"<path id=\"border-32-54\" d=\"{SvgPaths.Border32_54}\" transform=\"translate(102.000000, 517.500000) scale(-1, 1) translate(102.000000, -517.500000)\"></path>");
        sb.Append($"<path id=\"border-35-36\" d=\"{SvgPaths.Border35_36}\"></path>");
        sb.Append($"<path id=\"border-12-22\" d=\"{SvgPaths.Border12_22}\" transform=\"translate(296.867675, 325.715182) rotate(4.000000) translate(-296.867675, -325.715182)\"></path>");
        sb.Append($"<path id=\"border-16-48\" d=\"{SvgPaths.Border16_48}\" transform=\"translate(77.500000, 301.500000) scale(-1, 1) translate(-77.500000, -301.500000)\"></path>");
        sb.Append($"<path id=\"border-45-21\" d=\"{SvgPaths.Border45_21}\" transform=\"translate(263.557373, 285.393405) rotate(4.000000) translate(-263.557373, -285.393405)\"></path>");
        sb.Append($"<path id=\"border-50-6\" d=\"{SvgPaths.Border50_6}\" transform=\"translate(195.695801, 459.830417) rotate(123.000000) translate(-195.695801, -459.830417)\"></path>");
        sb.Append("</g>");

        // Helper for Gate Labels
        void AddGateLabel(int gate, double x, double y)
        {
            bool active = gates[gate] != "none";
            string fill = active ? "#000000" : "#FFFFFF";
            string textFill = active ? "#FFFFFF" : "#000000";
            sb.Append($"<rect fill=\"{fill}\" x=\"{x}\" y=\"{y}\" width=\"18\" height=\"15\" rx=\"7.5\"></rect>");
            sb.Append($"<text fill=\"{textFill}\" font-size=\"11\" font-weight=\"normal\"><tspan x=\"{x + 3}\" y=\"{y + 11}\">{gate}</tspan></text>");
        }

        // --- Centers ---

        // Root
        sb.Append("<g transform=\"translate(158.000000, 618.000000)\">");
        sb.Append($"<rect fill=\"{GetCenterFill(testData.DesignKuda, ROOT_COLOR)}\" stroke=\"black\" stroke-width=\"2\" x=\"0\" y=\"0\" width=\"83\" height=\"83\" rx=\"10\" transform=\"translate(41.5, 41.5) rotate(90) translate(-41.5, -41.5)\"></rect>");
        AddGateLabel(60, 32, 3);
        AddGateLabel(58, 3, 59);
        AddGateLabel(41, 61, 58);
        AddGateLabel(39, 62, 38);
        AddGateLabel(19, 63, 18);
        AddGateLabel(52, 52, 3);
        AddGateLabel(53, 10, 3);
        AddGateLabel(54, 2, 18);
        AddGateLabel(38, 3, 38);
        sb.Append("</g>");

        // Sacral
        sb.Append("<g transform=\"translate(158.000000, 507.000000)\">");
        sb.Append($"<rect fill=\"{GetCenterFill(testData.DesignSakral, SACRAL_COLOR)}\" stroke=\"black\" stroke-width=\"2\" x=\"0\" y=\"0\" width=\"83\" height=\"83\" rx=\"10\" transform=\"translate(41.5, 41.5) rotate(90) translate(-41.5, -41.5)\"></rect>");
        AddGateLabel(14, 32, 2);
        AddGateLabel(29, 53, 2);
        AddGateLabel(5, 12, 2);
        AddGateLabel(34, 4, 19);
        AddGateLabel(27, 2, 51);
        AddGateLabel(42, 11, 66);
        AddGateLabel(9, 55, 66);
        AddGateLabel(3, 35, 66);
        AddGateLabel(59, 63, 52);
        sb.Append("</g>");

        // Solar Plexus
        sb.Append("<g transform=\"translate(313.000000, 495.000000)\">");
        sb.Append($"<path fill=\"{GetCenterFill(testData.DesignPlexus, PLEXUS_COLOR)}\" d=\"{SvgPaths.PlexusPath}\" stroke=\"black\" stroke-width=\"2\" transform=\"translate(43, 47) scale(-1, 1) translate(-43, -47)\"></path>");
        AddGateLabel(36, 65, 4);
        AddGateLabel(22, 47, 15);
        AddGateLabel(37, 30, 24);
        AddGateLabel(6, 7, 40);
        AddGateLabel(49, 23, 51);
        AddGateLabel(55, 44, 64);
        AddGateLabel(30, 62, 73);
        sb.Append("</g>");

        // Heart
        sb.Append("<g transform=\"translate(253.000000, 422.000000)\">");
        sb.Append($"<path fill=\"{GetCenterFill(testData.DesignJantung, HEART_COLOR)}\" d=\"{SvgPaths.HeartPath}\" stroke=\"black\" stroke-width=\"2\" transform=\"translate(38.5, 31) scale(-1, 1) translate(-38.5, -31)\"></path>");
        AddGateLabel(21, 37, 5);
        AddGateLabel(26, 4, 33);
        AddGateLabel(51, 23, 17);
        AddGateLabel(40, 55, 44);
        sb.Append("</g>");

        // Spleen
        sb.Append("<g transform=\"translate(47.000000, 495.000000)\">");
        sb.Append($"<path fill=\"{GetCenterFill(testData.DesignLimpa, SPLEEN_COLOR)}\" d=\"{SvgPaths.SpleenPath}\" stroke=\"black\" stroke-width=\"2\"></path>");
        AddGateLabel(50, 65, 41);
        AddGateLabel(32, 40, 55);
        AddGateLabel(28, 19, 65);
        AddGateLabel(18, 2, 74);
        AddGateLabel(48, 2, 4);
        AddGateLabel(57, 18, 14);
        AddGateLabel(44, 38, 25);
        sb.Append("</g>");

        // G Center
        sb.Append("<g transform=\"translate(150.000000, 341.000000)\">");
        sb.Append($"<path fill=\"{GetCenterFill(testData.DesignJati, G_COLOR)}\" d=\"{SvgPaths.GCenterPath}\" stroke=\"black\" stroke-width=\"2\" transform=\"translate(59.5, 59.57) rotate(45) translate(-59.5, -59.57)\"></path>");
        AddGateLabel(1, 52, 9);
        AddGateLabel(7, 36, 25);
        AddGateLabel(13, 64, 23);
        AddGateLabel(25, 92, 51);
        AddGateLabel(10, 8, 51);
        AddGateLabel(15, 32, 77);
        AddGateLabel(2, 53, 95);
        AddGateLabel(46, 66, 79);
        sb.Append("</g>");

        // Throat
        sb.Append("<g transform=\"translate(158.000000, 244.000000)\">");
        sb.Append($"<rect fill=\"{GetCenterFill(testData.DesignTenggorokan, THROAT_COLOR)}\" stroke=\"black\" stroke-width=\"2\" x=\"0\" y=\"0\" width=\"83\" height=\"83\" rx=\"10\" transform=\"translate(41.5, 41.5) rotate(90) translate(-41.5, -41.5)\"></rect>");
        AddGateLabel(8, 34, 66);
        AddGateLabel(33, 51, 66);
        AddGateLabel(31, 12, 66);
        AddGateLabel(20, 3, 41);
        AddGateLabel(16, 3, 20);
        AddGateLabel(62, 11, 2);
        AddGateLabel(23, 31, 2);
        AddGateLabel(56, 51, 2);
        AddGateLabel(35, 62, 16);
        AddGateLabel(12, 62, 36);
        AddGateLabel(45, 63, 52);
        sb.Append("</g>");

        // Ajna
        sb.Append("<g transform=\"translate(156.000000, 119.000000)\">");
        sb.Append($"<path fill=\"{GetCenterFill(testData.DesignAjna, AJNA_COLOR)}\" d=\"{SvgPaths.AjnaPath}\" stroke=\"black\" stroke-width=\"2\" transform=\"translate(44, 40) rotate(90) translate(-44, -40)\"></path>");
        AddGateLabel(24, 34, 2);
        AddGateLabel(47, 11, 2);
        AddGateLabel(4, 56, 2);
        AddGateLabel(17, 19, 34);
        AddGateLabel(43, 35, 61);
        AddGateLabel(11, 50, 34);
        sb.Append("</g>");

        // Head
        sb.Append("<g transform=\"translate(156.000000, 8.000000)\">");
        sb.Append($"<path fill=\"{GetCenterFill(testData.DesignKepala, HEAD_COLOR)}\" d=\"{SvgPaths.CrownPath}\" stroke=\"black\" stroke-width=\"2\" transform=\"translate(44, 40) scale(-1, 1) rotate(-90) translate(-44, -40)\"></path>");
        AddGateLabel(64, 13, 62);
        AddGateLabel(61, 34, 62);
        AddGateLabel(63, 56, 62);
        sb.Append("</g>");

        sb.Append("</g></svg>");
        return sb.ToString();
    }
}
