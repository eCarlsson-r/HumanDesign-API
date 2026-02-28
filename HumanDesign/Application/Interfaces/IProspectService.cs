using HumanDesign.Domain.Models.Requests;
using HumanDesign.Infrastructure.Entities.Charts;
namespace HumanDesign.Application.Interfaces;
public interface IProspectService
{
    Task<Guid> CreateProspectAsync(CreateProspectRequest request);
    Task<Design> GetReportAsync(Guid prospectId);
}