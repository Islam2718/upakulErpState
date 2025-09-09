using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using HRM.Application.Features.DBOrders.Commands.Delete.Commands;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;
using HRM.Domain.ViewModels;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using HRM.Application.Features.DBOrders.Queries.LeaveSetup;

namespace HRM.Api.Controllers
{
    public class LeaveSetupController : ApiController
    {
        IMediator _mediator;
    

        public LeaveSetupController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateLeaveSetupCommand request)
        {
            try
            {
                request.CreatedBy = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
       
            [HttpGet]
        [ProducesResponseType(typeof(LeaveSetupVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new LeaveSetupByIdQuery(id));
            return CustomResult(obj);
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateLeaveSetupCommand request)
        {
            try
            {
                request.UpdatedBy = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromBody] DeleteLeaveSetupCommand request)
        {
            try
            {

                request.DeletedBy = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet]
        public async Task<IActionResult> LoadGrid(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 0,
        [FromQuery] string search = "",
        [FromQuery] string SortOrder = "")
        {
            var query = new LeaveSetupGridQuery
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
                SortOrder = SortOrder,
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }
       
    }
}
