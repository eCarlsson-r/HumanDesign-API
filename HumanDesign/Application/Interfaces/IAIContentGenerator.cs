namespace HumanDesign.Application.Interfaces;
public interface IAIContentGenerator
{
    Task<string?> GenerateAsync(string contentType, string key, string level);
}