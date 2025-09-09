using System.ComponentModel.DataAnnotations.Schema;


namespace MF.Domain.Models.View
{
    [Table("vw_LoanForm", Schema = "loan")]
    public class VWLoanForm
    {
        public long LoanApplicationId { get; set; }
        public long? SummaryId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string ApplicationNo { get; set; }
        public DateTime? DisburseDate {  get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string? GroupCode { get; set; }
        public string? GroupName { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string MemberCode { get; set; }
        public string MemberName { get; set; }
        public string? ContactNoOwn { get; set; }
        public string? NationalId { get; set; }
        public string? SpouseName { get; set; }
        public string? MobileNumber { get; set; }
        public int? TotalIncome { get; set; }
        public int? Emp_SelfFullTimeMale { get; set; }
        public int? Emp_SelfFullTimeFemale { get; set; }
        public int? Emp_SelfPartTimeMale { get; set; }
        public int? Emp_SelfPartTimeFemale { get; set; }
        public int? Emp_WageFullTimeMale { get; set; }
        public int? Emp_WageFullTimeFemale { get; set; }
        public int? Emp_WagePartTimeMale { get; set; }
        public int? Emp_WagePartTimeFemale { get; set; }
        public int? PhaseNumber { get; set; }
        public string? PurposeName { get; set; }
        public string? Component { get; set; }
        public int PrincipleAmount {  get; set; }
        public int ProposedAmount { get; set; }
        public string? ApplicationStatus { get; set; }
    }
}
