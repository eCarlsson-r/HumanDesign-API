using System.Text.Json.Serialization;

namespace HumanDesignApi.Models;

public class LegacyHumanDesignResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("dob")]
    public string Dob { get; set; } = string.Empty;

    [JsonPropertyName("tob")]
    public string Tob { get; set; } = string.Empty;

    [JsonPropertyName("design-datetime")]
    public string DesignDateTime { get; set; } = string.Empty;

    [JsonPropertyName("design-type")]
    public string DesignType { get; set; } = string.Empty;

    [JsonPropertyName("design-strategy")]
    public string DesignStrategy { get; set; } = string.Empty;

    [JsonPropertyName("design-characteristic")]
    public string DesignCharacteristic { get; set; } = string.Empty;

    [JsonPropertyName("design-theme")]
    public string DesignTheme { get; set; } = string.Empty;

    [JsonPropertyName("design-authority")]
    public string DesignAuthority { get; set; } = string.Empty;

    [JsonPropertyName("design-circuit")]
    public string DesignCircuit { get; set; } = string.Empty;

    [JsonPropertyName("design-profile")]
    public string DesignProfile { get; set; } = string.Empty;

    [JsonPropertyName("design-cross-type")]
    public string DesignCrossType { get; set; } = string.Empty;

    [JsonPropertyName("design-cross")]
    public string DesignCross { get; set; } = string.Empty;

    [JsonPropertyName("cross-name")]
    public string CrossName { get; set; } = string.Empty;

    [JsonPropertyName("design-digestion")]
    public string DesignDigestion { get; set; } = string.Empty;

    [JsonPropertyName("design-reasoning")]
    public string DesignReasoning { get; set; } = string.Empty;

    [JsonPropertyName("design-cognition")]
    public string DesignCognition { get; set; } = string.Empty;

    [JsonPropertyName("design-motivation")]
    public string DesignMotivation { get; set; } = string.Empty;

    [JsonPropertyName("design-perspective")]
    public string DesignPerspective { get; set; } = string.Empty;

    [JsonPropertyName("design-awareness")]
    public string DesignAwareness { get; set; } = string.Empty;

    [JsonPropertyName("design-environment")]
    public string DesignEnvironment { get; set; } = string.Empty;

    // Unconscious (Design) Gates
    [JsonPropertyName("design-uc01")] public string DesignUc01 { get; set; } = string.Empty;
    [JsonPropertyName("design-uc02")] public string DesignUc02 { get; set; } = string.Empty;
    [JsonPropertyName("design-uc03")] public string DesignUc03 { get; set; } = string.Empty;
    [JsonPropertyName("design-uc04")] public string DesignUc04 { get; set; } = string.Empty;
    [JsonPropertyName("design-uc05")] public string DesignUc05 { get; set; } = string.Empty;
    [JsonPropertyName("design-uc06")] public string DesignUc06 { get; set; } = string.Empty;
    [JsonPropertyName("design-uc07")] public string DesignUc07 { get; set; } = string.Empty;
    [JsonPropertyName("design-uc08")] public string DesignUc08 { get; set; } = string.Empty;
    [JsonPropertyName("design-uc09")] public string DesignUc09 { get; set; } = string.Empty;
    [JsonPropertyName("design-uc10")] public string DesignUc10 { get; set; } = string.Empty;
    [JsonPropertyName("design-uc11")] public string DesignUc11 { get; set; } = string.Empty;
    [JsonPropertyName("design-uc12")] public string DesignUc12 { get; set; } = string.Empty;
    [JsonPropertyName("design-uc13")] public string DesignUc13 { get; set; } = string.Empty;
    [JsonPropertyName("design-uc14")] public string DesignUc14 { get; set; } = string.Empty;
    [JsonPropertyName("design-uc15")] public string DesignUc15 { get; set; } = string.Empty;

    // Centers
    [JsonPropertyName("design-kepala")] public string DesignKepala { get; set; } = string.Empty;
    [JsonPropertyName("design-ajna")] public string DesignAjna { get; set; } = string.Empty;
    [JsonPropertyName("design-tenggorokan")] public string DesignTenggorokan { get; set; } = string.Empty;
    [JsonPropertyName("design-jati")] public string DesignJati { get; set; } = string.Empty;
    [JsonPropertyName("design-jantung")] public string DesignJantung { get; set; } = string.Empty;
    [JsonPropertyName("design-plexus")] public string DesignPlexus { get; set; } = string.Empty;
    [JsonPropertyName("design-limpa")] public string DesignLimpa { get; set; } = string.Empty;
    [JsonPropertyName("design-sakral")] public string DesignSakral { get; set; } = string.Empty;
    [JsonPropertyName("design-kuda")] public string DesignKuda { get; set; } = string.Empty;

    // Conscious (Personality) Gates
    [JsonPropertyName("design-c01")] public string DesignC01 { get; set; } = string.Empty;
    [JsonPropertyName("design-c02")] public string DesignC02 { get; set; } = string.Empty;
    [JsonPropertyName("design-c03")] public string DesignC03 { get; set; } = string.Empty;
    [JsonPropertyName("design-c04")] public string DesignC04 { get; set; } = string.Empty;
    [JsonPropertyName("design-c05")] public string DesignC05 { get; set; } = string.Empty;
    [JsonPropertyName("design-c06")] public string DesignC06 { get; set; } = string.Empty;
    [JsonPropertyName("design-c07")] public string DesignC07 { get; set; } = string.Empty;
    [JsonPropertyName("design-c08")] public string DesignC08 { get; set; } = string.Empty;
    [JsonPropertyName("design-c09")] public string DesignC09 { get; set; } = string.Empty;
    [JsonPropertyName("design-c10")] public string DesignC10 { get; set; } = string.Empty;
    [JsonPropertyName("design-c11")] public string DesignC11 { get; set; } = string.Empty;
    [JsonPropertyName("design-c12")] public string DesignC12 { get; set; } = string.Empty;
    [JsonPropertyName("design-c13")] public string DesignC13 { get; set; } = string.Empty;
    [JsonPropertyName("design-c14")] public string DesignC14 { get; set; } = string.Empty;
    [JsonPropertyName("design-c15")] public string DesignC15 { get; set; } = string.Empty;

    // Arrows/Variables
    [JsonPropertyName("design-environment-arrow")] public string DesignEnvironmentArrow { get; set; } = string.Empty;
    [JsonPropertyName("design-digestion-arrow")] public string DesignDigestionArrow { get; set; } = string.Empty;
    [JsonPropertyName("design-perspective-arrow")] public string DesignPerspectiveArrow { get; set; } = string.Empty;
    [JsonPropertyName("design-awareness-arrow")] public string DesignAwarenessArrow { get; set; } = string.Empty;

    // Channels (up to some max, e.g., 36)
    // The PHP code uses dynamic keys like "design-channel1", "channel1-name"
    // We can use a Dictionary for these or just define several if there's a practical limit.
    [JsonExtensionData]
    public Dictionary<string, object>? AdditionalData { get; set; }
}
