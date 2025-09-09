using MediatR;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Handler.CodeGenerator
{
    public class UpdateCodeGeneratorListCommandHandler : IRequestHandler<UpdateCodeGeneratorListCommand, CommadResponse>
    {
        private readonly IMediator _mediator;

        public UpdateCodeGeneratorListCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CommadResponse> Handle(UpdateCodeGeneratorListCommand request, CancellationToken cancellationToken)
        {
            int successCount = 0;
            int failCount = 0;

            foreach (var item in request.Items)
            {
                try
                {
                    var response = await _mediator.Send(item, cancellationToken);
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                        successCount++;
                    else
                        failCount++;
                }
                catch
                {
                    failCount++;
                }
            }

            string message = $"Successfully updated: {successCount}, Failed: {failCount}";

            return new CommadResponse(message, HttpStatusCode.OK);
        }
    }
}
