using HRM.Application.Features.DBOrders.Commands.Create.Commands.Test;
using HRM.Application.Features.DBOrders.Commands.Update.Commands.Test;
using MediatR;
using Message.Library.Contacts.Repository;
using Message.Library.Model;
using Message.Library.Template;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;

namespace HRM.Api.Controllers.Test
{
    
    [ApiController]
    public class FileUploadTestController : ApiController
    {
        IMediator _mediator;
        public FileUploadTestController(IMediator mediator) 
        {
         _mediator = mediator;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromForm] CreateFileUploadTestCommand request)
        {
            //var fileBytes =  Convert.FromBase64String(request.NIDPicUrl);
            //var savePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", "ddd.jpg");

            //Directory.CreateDirectory(Path.GetDirectoryName(savePath));

            //System.IO.File.WriteAllBytes(savePath, fileBytes);

            await _mediator.Send(request);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFileUploadTestCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPost]

        public async Task<IActionResult> SendMail()
        {
            var t = new EmailBodyCommonTemplate().LoanApplicationApproval("Principal", "000", "Abdul Baten", "Bhola", "01 jun 2025", "100000", "google.com", "from 14 jun to 15 jun",100000,"20 Jan 2025","26 Apr 2025","0054","Abdul Rahman");
            var mail = new EmailMessage() {
                To = "mahfuz@coastbd.net",
                ToDisplayName = "Mahfuz Morshed",
                CC = "mahfuzmorshed99@gmail.com",
                CCDisplayName = "Mahfuz Morshed GM",
                Body = t.Item2,
                Subject = t.Item1,
            };
            await new EmailService().SendEmailAsync(mail);
            return Ok();
        }
    }
}
