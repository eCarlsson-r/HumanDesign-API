using HumanDesign.Domain.Models.Requests;
namespace HumanDesign.Application.Interfaces;
public interface IProspectService
{
    Task<Guid> CreateProspectAsync(CreateProspectRequest request, Guid ownerId);
}