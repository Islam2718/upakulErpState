using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Delete.Command
{
    public class DeleteLoanApprovalCommand : IRequest<CommadResponse>
    {
        public int Lavel { get; set; }
    }
}
