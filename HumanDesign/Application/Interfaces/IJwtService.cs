using HumanDesign.Infrastructure.Entities;
namespace HumanDesign.Application.Interfaces;
public interface IJwtService
{
    string GenerateToken(UserEntity user);
}