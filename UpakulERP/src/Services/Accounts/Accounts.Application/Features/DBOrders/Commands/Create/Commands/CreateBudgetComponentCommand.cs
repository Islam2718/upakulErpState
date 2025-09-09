using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace Accounts.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateBudgetComponentCommand : IRequest<CommadResponse>
    {
        public int? ParentId { get; set; }
        public string ComponentName { get; set; }
        public string? LabelShow { get; set; }
        public bool? IsMedical { get; set; }
        public bool? IsDesignation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }
}
