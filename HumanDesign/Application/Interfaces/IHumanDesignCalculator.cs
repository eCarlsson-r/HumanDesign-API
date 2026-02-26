using HumanDesign.Infrastructure.Entities;
using HumanDesign.Infrastructure.Entities.Charts;

namespace HumanDesign.Application.Interfaces;
public interface IHumanDesignCalculator
{
    Task<Design> GenerateDesignAsync(Prospect prospect);
}