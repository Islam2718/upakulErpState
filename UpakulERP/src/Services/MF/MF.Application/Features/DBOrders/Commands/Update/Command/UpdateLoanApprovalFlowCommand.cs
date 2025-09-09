using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Command
{
    public class UpdateLoanApprovalFlowCommand : IRequest<CommadResponse>
    {
        public long LoanApplicationId { get; set; }
        public int ProposedAmount { get; set; }

        public string ActionType { get; set; }/*APPROVED,REJECTED,REVISED,DISBURSED*/


        // When DISBURSED
        public string? PaymentType { get; set; }
        public int? BankId { get; set; }
        //public int? BankBranchId { get; set; }
        public string? ChequeNo { get; set; }
        public string? ReferenceNo { get; set; }
        // For DISBURSED 
        public string? Note { get; set; }
        public int? loggedInEmpId { get; set; }
        public DateTime? transactionDate { get; set; }
        public int? loggedInOfficeTypeId { get; set; }
        public int? loggedInOfficeId { get; set; }

    }
}
