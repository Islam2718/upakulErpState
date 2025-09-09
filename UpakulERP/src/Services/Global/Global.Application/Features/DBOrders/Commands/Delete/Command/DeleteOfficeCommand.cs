using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Delete.Command
{
    public class DeleteOfficeCommand : IRequest<CommadResponse>
    {
        public int OfficeId { get; set; }
        public bool IsActive { get; set; } = false;

        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; } = DateTime.Now;
    }

}
