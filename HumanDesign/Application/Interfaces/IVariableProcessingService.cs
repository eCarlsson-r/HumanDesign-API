namespace HumanDesign.Application.Interfaces;
public interface IVariableProcessingService
{
    Task<Dictionary<string, string>> ProcessVariablesAsync(Guid designId);
}