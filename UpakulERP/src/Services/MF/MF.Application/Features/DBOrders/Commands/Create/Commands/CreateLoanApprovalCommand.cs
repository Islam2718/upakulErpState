using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateLoanApprovalCommand : IRequest<CommadResponse>
    {
        public int Level { get; set; }
        public string ApprovalType { get; set; }
        public int DesignationId { get; set; }
        //public string Method { get; set; }
        public int StartingValueAmount { get; set; }
    }
}
