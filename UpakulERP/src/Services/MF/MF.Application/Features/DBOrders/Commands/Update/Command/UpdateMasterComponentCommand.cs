using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Command
{
    public class UpdateMasterComponentCommand :IRequest<CommadResponse>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
