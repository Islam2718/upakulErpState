using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Domain.ViewModels
{
    public class LoadGridForLoanApproveVM
    {
        public long LoanApplicationId { get; set; }
        public string? ApplicationNo { get; set; }
        public string? MemberName { get; set; }
        public string? OfficeName { get; set; }
        public string? MemberCode { get; set; }
        public int MemberId { get; set; }
        public int ComponentId { get; set; }
        public int PurposeId { get; set; }
        public int ProposedBy { get; set; }
        public int ProposedAmount { get; set; }
        public int? ApprovedAmount { get; set; }
        public DateTime ApplicationDate { get; set; }

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
        public int? CheckedBy { get; set; }
        public DateTime? CheckedDate { get; set; }
        public int? FirstApprovedBy { get; set; }
        public DateTime? FirstApproveddate { get; set; }
        public int? SecendApprovedBy { get; set; }
        public DateTime? SecendApproveddate { get; set; }
        public int? ThirdApprovedBy { get; set; }
        public DateTime? ThirdApproveddate { get; set; }
        public int? FourthApprovedBy { get; set; }
        public DateTime? FourthApproveddate { get; set; }
    }
}
