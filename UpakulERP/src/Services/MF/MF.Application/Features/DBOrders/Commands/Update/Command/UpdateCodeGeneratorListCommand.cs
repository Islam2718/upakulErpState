using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Command
{
    public class UpdateCodeGeneratorListCommand : IRequest<CommadResponse>
    {
        public List<UpdateCodeGeneratorCommand> Items { get; set; }
    }
}
