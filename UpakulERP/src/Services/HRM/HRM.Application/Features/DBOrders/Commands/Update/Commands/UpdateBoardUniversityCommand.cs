using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Update.Commands
{
    public class UpdateBoardUniversityCommand : IRequest<CommadResponse>
    {
        public int BUId { get; set; }
        public string BUName { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }= DateTime.Now;
    }

}
