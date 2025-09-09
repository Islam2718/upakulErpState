using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Delete.Command
{
    public class DeleteComponentCommand : IRequest<CommadResponse>
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = false;
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
