using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Commands
{
    public class InitialDayProcessCommand : IRequest<CommadResponse>
    {
        public int? OfficeId { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime InitialDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public Boolean IsDayClose { get; set; }
        public DateTime? ReOpenDate { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }


}
