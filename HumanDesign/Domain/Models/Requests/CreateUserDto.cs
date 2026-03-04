namespace HumanDesign.Domain.Models.Requests;
public class CreateUserDto
{
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string Role { get; set; } = ""; // Leader or Agent
}