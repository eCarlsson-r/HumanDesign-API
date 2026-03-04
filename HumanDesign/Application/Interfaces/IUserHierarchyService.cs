namespace HumanDesign.Application.Interfaces;
public interface IUserHierarchyService
{
    Task<List<Guid>> GetDescendantUserIdsAsync(Guid userId);
}