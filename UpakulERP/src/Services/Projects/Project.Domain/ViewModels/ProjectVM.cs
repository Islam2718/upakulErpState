namespace Project.Domain.ViewModels
{
    public class ProjectVM
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectType { get; set; }
        public string Objective { get; set; }
        public string DonerName {  get; set; }
        public int ChipEmployeeId { get; set; }
        public string ChipEmployee { get; set; }
        public int TotalStaff { get; set; }
        public int MonitoringPeriod { get; set; }
        public string Target { get; set; }
        public string TotalTarget { get; set; }
        public DateTime? StartMonth { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string ProjectShortName { get; set; }
        public string TargetType { get; set; }
        public string MonthlyQuarterly { get; set; }
        public string FinancialTarget { get; set; }
    }
}
