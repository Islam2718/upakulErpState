using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Domain.ViewModels
{
    public class LoanApplicationVM
    {
        public long LoanApplicationId { get; set; }
        public string ApplicationNo { get; set; }
        //public int OfficeId { get; set; }
        public string MemberName { get; set; }
        public string? GroupName { get; set; }
        public int? PhaseNumber { get; set; }
        //public int ComponentId { get; set; }
        public string PurposeName { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ProposedBy { get; set; }
        public int ProposedAmount { get; set; }
        //public bool IsActive { get; set; }
        public int? FirstGuarantorMemberId { get; set; }
        public string FirstGuarantorName { get; set; }
        public string FirstGuarantorContactNo { get; set; }
        public string FirstGuarantorRelation { get; set; }
        public string? FirstGuarantorRemark { get; set; }
        public int? SecendGuarantorMemberId { get; set; }
        public string SecendGuarantorName { get; set; }
        public string SecendGuarantorContactNo { get; set; }
        public string SecendGuarantorRelation { get; set; }
        public string? SecendGuarantorRemark { get; set; }
        public string ApplicationStatus { get; set; }
        public int? ApprovedLevel { get; set; }
        public int? Emp_SelfFullTimeMale { get; set; }
        public int? Emp_SelfFullTimeFemale { get; set; }
        public int? Emp_SelfPartialTimeMale { get; set; }
        public int? Emp_SelfPartialTimeFemale { get; set; }
        public int? Emp_SalaryFullTimeMale { get; set; }
        public int? Emp_SalaryFullTimeFemale { get; set; }
        public int? Emp_SalaryPartialTimeMale { get; set; }
        public int? Emp_SalaryPartialTimeFemale { get; set; }
        public int? CheckedBy { get; set; }
        public DateTime? CheckedDate { get; set; }
        public int? CheckerProposedAmount { get; set; }
        public int? FirstApprovedBy { get; set; }
        public DateTime? FirstApproveddate { get; set; }
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
        public string? LoneeGroupImgUrl { get; set; }
    }
}
