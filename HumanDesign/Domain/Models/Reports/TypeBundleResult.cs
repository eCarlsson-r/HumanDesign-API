namespace HumanDesign.Domain.Models.Reports;
public class TypeBundleResult
{
    public ResolvedContent Type { get; set; } = default!;
    public ResolvedContent Strategy { get; set; } = default!;
    public ResolvedContent Signature { get; set; } = default!;
    public ResolvedContent NotSelf { get; set; } = default!;
}