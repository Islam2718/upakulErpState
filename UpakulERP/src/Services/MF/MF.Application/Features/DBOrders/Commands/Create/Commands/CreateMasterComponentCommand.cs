using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Commands
{
   public class CreateMasterComponentCommand : IRequest<CommadResponse>
    {

        public string Name { get; set; }
        public string Code { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
