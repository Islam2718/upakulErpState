using MediatR;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Handler.CodeGenerator
{
    public class UpdateCodeGeneratorListHandler : IRequestHandler<UpdateCodeGeneratorListCommand, CommadResponse>
    {
        public Task<CommadResponse> Handle(UpdateCodeGeneratorListCommand request, CancellationToken cancellationToken)
        {
            foreach (var cmd in request.Items)
            {
                // Your update logic here (e.g., update DB records)
            }
            return null;
            //return new CustomResponse
            //{
            //    StatusCode = HttpStatusCode.OK,
            //    Message = "All updates successful"
            //};
        }
    }
}
