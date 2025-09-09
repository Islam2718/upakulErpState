using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace Accounts.Application.Features.DBOrders.Commands.Update.Command
{
    public class UpdateBudgetComponentCommand : IRequest<CommadResponse>
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string ComponentName { get; set; }
        public bool? IsMedical { get; set; }
        public bool? IsDesignation { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
