using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateGraceScheduleCommand : IRequest<CommadResponse>
 
    {
        public string Reason { get; set; }
        public int? OfficeId { get; set; }
        public int? GroupId { get; set; }
        public int? LoanId { get; set; }
        public string GraceFrom { get; set; }
        public string GraceTo { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }
}
