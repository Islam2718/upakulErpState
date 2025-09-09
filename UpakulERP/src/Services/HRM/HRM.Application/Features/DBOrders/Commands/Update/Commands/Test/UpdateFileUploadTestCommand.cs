using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Update.Commands.Test
{
    public class UpdateFileUploadTestCommand : IRequest<CommadResponse>
    {
        public int Id { get; set; }
        public string Purpose { get; set; }
    }
}
