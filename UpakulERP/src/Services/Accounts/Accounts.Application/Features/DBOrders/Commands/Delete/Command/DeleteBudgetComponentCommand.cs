using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace Accounts.Application.Features.DBOrders.Commands.Delete.Command
{
    public class DeleteBudgetComponentCommand : IRequest<CommadResponse>
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = false;
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }


}
