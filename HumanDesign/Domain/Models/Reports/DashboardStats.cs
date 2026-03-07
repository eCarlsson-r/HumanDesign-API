namespace HumanDesign.Domain.Models.Reports;
public class DashboardStats {
    public int Prospects { get; set; }
    public int TeamMembers { get; set; }
    public int ReportsGenerated { get; set; }
    public int TodayProspects { get; set; }
    public int MyProspects { get; set; }
    public required List<ChartStat> ChartStats { get; set; }
}

public class ChartStat {
    public DateTime Date { get; set; }
    public int Count { get; set; }
}