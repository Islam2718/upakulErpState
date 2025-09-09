using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility.CommonController;

namespace HRM.Api.Controllers
{
    public class EmployeeTypeController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public EmployeeTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeTypeCommand request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.EmployeeTypeName))
                    return CustomResult("Employee type is required.", HttpStatusCode.BadRequest);
                else
                {
                    request.CreatedBy = loggedInEmployeeId;
                    request.CreatedOn = DateTime.Now;
                    var response = await _mediator.Send(request);
                    return CustomResult(response.Message, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
