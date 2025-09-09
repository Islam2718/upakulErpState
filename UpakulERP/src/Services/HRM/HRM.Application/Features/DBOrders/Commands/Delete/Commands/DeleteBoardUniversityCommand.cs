using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Delete.Commands
{
    public class DeleteBoardUniversityCommand : IRequest<CommadResponse>
    {
        public int BUId { get; set; }
        public bool IsActive { get; set; } = false;
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }=DateTime.Now;
    }
}
