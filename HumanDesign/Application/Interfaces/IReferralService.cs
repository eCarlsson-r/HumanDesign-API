namespace HumanDesign.Application.Interfaces;
public interface IReferralService
{
    Task<Guid> ResolveOwnerIdAsync(string? referralCode);
}