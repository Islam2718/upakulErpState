using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Commands
{
  public  class CreateOfficeTypeXConfigureDetailsCommand : IRequest<CommadResponse>
    {
        public int ConfigureMasterId { get; set; }
        public int ApproverDesignationId { get; set; }
        public int LevelNo { get; set; }
        public int MinimumLeave { get; set; }
        public int? MaximumLeave { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }

}
