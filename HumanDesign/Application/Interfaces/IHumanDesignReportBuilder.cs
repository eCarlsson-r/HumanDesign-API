using HumanDesign.Domain.Models.Reports;

namespace HumanDesign.Application.Interfaces;

public interface IHumanDesignReportBuilder
{
    Task<HumanDesignReport> BuildPreviewAsync(Guid designId);

    Task<HumanDesignReport> BuildSummaryAsync(Guid designId);

    Task<HumanDesignReport> BuildDetailAsync(Guid designId);
}