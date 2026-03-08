using Microsoft.AspNetCore.Mvc;

namespace HumanDesign.Application.Interfaces;
public interface IAuthService
{
    Task<IActionResult> RegisterAsync(string email, string password);
    Task<IActionResult> LoginAsync(string email, string password);
}