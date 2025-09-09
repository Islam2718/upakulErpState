using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Commands
{
   public class CreateLoanApprovalListCommand : IRequest<CommadResponse>
    {
        public List<CreateLoanApprovalCommand> LoanApprovals { get; set; }

        public CreateLoanApprovalListCommand(List<CreateLoanApprovalCommand> loanApprovals)
        {
            LoanApprovals = loanApprovals;
        }
    }    

}
