using MediatR;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;

namespace MF.API.Controllers
{
    public class DailyProcessController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public DailyProcessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] InitialDayProcessCommand request)
        {
            try
            {
               request.CreatedBy = loggedInEmployeeId;
               request.CreatedOn = DateTime.Now;
               request.OfficeId = loggedInOfficeId;
               var response = await _mediator.Send(request);
               return CustomResult(response.Message, response.StatusCode);               
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
