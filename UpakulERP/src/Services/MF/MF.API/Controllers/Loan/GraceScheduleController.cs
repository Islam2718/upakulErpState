using System.Net;
using MediatR;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Application.Features.DBOrders.Queries.GraceSchedule;
using MF.Domain.Models.View;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using Utility.Response;


namespace MF.API.Controllers.Loan
{

    public class GraceScheduleController : ApiController
    {
        IMediator _mediator;

        public GraceScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateGraceScheduleCommand request)
        {
            try
            {
                request.CreatedBy = (int)loggedInEmployeeId;
                var response = await _mediator.Send(request);
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateGraceScheduleCommand request)
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

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Approved([FromBody] UpdateGraceScheduleApprovedCommand request)
        {
            try
            {
                request.ApprovedBy = loggedInEmployeeId;
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
        public async Task<IActionResult> Delete([FromBody] DeleteGraceScheduleCommand request)
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
        [ProducesResponseType(typeof(VWGraceSchedule), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new GraceScheduleByIdQuery(id));
            return CustomResult(obj);
        }

        [ProducesResponseType(typeof(PaginatedResponse<VWGraceSchedule>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoadGrid(
                   [FromQuery] int page = 1,
                   [FromQuery] int pageSize = 0,
                   [FromQuery] string search = "",
                   [FromQuery] string SortOrder = "")
        {
            // ✅ Debugging log
            //Console.WriteLine($"Received Parameters -> Page: {page}, PageSize: {pageSize}, Search: '{search}', SortColumn: '{sortColumn}', SortDirection: '{sortDirection}'");
            var query = new GraceScheduleGridQuery
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
