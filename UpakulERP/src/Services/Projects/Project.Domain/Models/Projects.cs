using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using Utility.Domain;

namespace Project.Domain.Models
{
    
    [Table("Project", Schema = "dbo")]
    public class Projects : EntityBase
    {
        [Key]
        public int ProjectId { get; set; }
        public int DonerId {  get; set; }
        public string ProjectShortName { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectType { get; set; }
        public string? Objective { get; set; }
        public int ChipEmployeeId { get; set; }
        public int TotalStaff { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public int? MonitoringPeriod { get; set; }
        public string? Target { get; set; }
        public string? TotalTarget { get; set; }
        public string? TargetType { get; set; }
        public string? MonthlyQuarterly { get; set; }
        public string? FinancialTarget { get; set; }
    }

}
