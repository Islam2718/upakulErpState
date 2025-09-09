using MediatR;
using Microsoft.AspNetCore.Http;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Commands.Test
{
    public class CreateFileUploadTestCommand : IRequest<CommadResponse>
    {
        public string Purpose { get; set; }
        public List<IFormFile>? formFile { get; set; }
    }
}
