using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Command
{
    public class UpdateGraceScheduleCommand : IRequest<CommadResponse>
    {
        public int Id { get; set; } // primary key
        public string Reason { get; set; }
        public int? OfficeId { get; set; }
        public int? GroupId { get; set; }
        public int? LoanId { get; set; }
        public string GraceFrom { get; set; }
        public string GraceTo { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;

    }
}
