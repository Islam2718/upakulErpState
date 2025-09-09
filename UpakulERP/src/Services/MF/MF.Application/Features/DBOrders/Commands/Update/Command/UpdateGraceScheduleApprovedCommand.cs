using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Command
{
    public class UpdateGraceScheduleApprovedCommand : IRequest<CommadResponse>
    {
        public int Id { get; set; } // primary key
        public bool IsApproved { get; set; } = true;
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; } = DateTime.Now;

    }
}
