using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;

namespace MF.Domain.Models.Loan
{
    [Table("LoanApplication", Schema = "loan")]
    public class LoanApplication : EntityBase
    {
        [Key]
        public long LoanApplicationId { get; set; }
        public string ApplicationNo { get; set; }
        public int OfficeId { get; set; }
        public int GroupId { get; set; }
        public int MemberId { get; set; }
        public int ComponentId { get; set; }
        public int? PhaseNumber { get; set; }
        public int PurposeId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string? LoneeGroupImgUrl { get; set; }
        public int ProposedBy { get; set; }
        public int ProposedAmount { get; set; }
        public int? Emp_SelfFullTimeMale { get; set; }
        public int? Emp_SelfFullTimeFemale { get; set; }
        public int? Emp_SelfPartTimeMale { get; set; }
        public int? Emp_SelfPartTimeFemale { get; set; }
        public int? Emp_WageFullTimeMale { get; set; }
        public int? Emp_WageFullTimeFemale { get; set; }
        public int? Emp_WagePartTimeMale { get; set; }
        public int? Emp_WagePartTimeFemale { get; set; }
        public int? FirstGuarantorMemberId { get; set; }
        public string FirstGuarantorName { get; set; }
        public string FirstGuarantorContactNo { get; set; }
        public string FirstGuarantorRelation { get; set; }
        public string? FirstGuarantorRemark { get; set; }
        public int? SecondGuarantorMemberId { get; set; }
        public string SecondGuarantorName { get; set; }
        public string SecondGuarantorContactNo { get; set; }
        public string SecondGuarantorRelation { get; set; }
        public string? SecondGuarantorRemark { get; set; }
        public string ApplicationStatus { get; set; }
        public int? ApprovedLevel { get; set; }
       
        public int? CheckedBy { get; set; }
        public DateTime? CheckedDate { get; set; }
        public int? CheckerProposedAmount { get; set; }
        public int? FirstApprovedBy { get; set; }
        public DateTime? FirstApprovedOn { get; set; }
        public int? FirstApprovedAmount { get; set; }
        public int? SecondApprovedBy { get; set; }
        public DateTime? SecondApprovedOn { get; set; }
        public int? SecondApprovedAmount { get; set; }
        public int? ThirdApprovedBy { get; set; }
        public DateTime? ThirdApprovedOn { get; set; }
        public int? ThirdApprovedAmount { get; set; }
        public int? FourthApprovedBy { get; set; }
        public DateTime? FourthApprovedOn { get; set; }
        public int? FourthApprovedAmount { get; set; }
        public int? DistributedBy { get; set; }
        public DateTime? DistributedOn { get; set; }
        public int? DistributedAmount { get; set; }

        public int? RejectedBy { get; set; }
        public DateTime? RejectedOn { get; set; }
        public string? RejectedNote { get; set; }
        public int? RevisedBy { get; set; }
        public DateTime? RevisedOn { get; set; }
        public string? RevisedNote { get; set; }

    }
}
