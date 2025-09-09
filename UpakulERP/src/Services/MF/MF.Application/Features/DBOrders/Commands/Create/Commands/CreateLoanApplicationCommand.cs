using MediatR;
using Microsoft.AspNetCore.Http;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateLoanApplicationCommand : IRequest<CommadResponse>
    {
        //public string? ApplicationNo { get; set; }
        public int OfficeId { get; set; }
        public int GroupId { get; set; }
        public int MemberId { get; set; }
        public int ComponentId { get; set; }
        //public int? PhaseNumber { get; set; }
        public int PurposeId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public IFormFile? LoneeGroupImg { get; set; }
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
        public bool IsActive { get; set; }=true;
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }    
}
