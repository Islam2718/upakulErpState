using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace Project.Application.Features.DBOrders.Commands.Delete.Commands
{

    public class DeleteDonerCommand : IRequest<CommadResponse>
    {
        public int DonerId { get; set; }
        public bool IsActive { get; set; } = false;
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
