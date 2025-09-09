using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using HRM.Application.Features.DBOrders.Queries.LeaveMapping;
using Utility.Response;
using HRM.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using HRM.Domain.Models;

namespace HRM.Api.Controllers
{
    public class LeaveMappingController : ApiController
    {
        private readonly IMediator _mediator;

        public LeaveMappingController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateWithDetails([FromBody] CreateOfficeTypeXConfigMasterCommand request)
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
        // ------- Master Create -------
        [HttpPost("CreateMaster")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateMaster([FromBody] CreateOfficeTypeXConfigMasterCommand request)
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

        // ------- Details Create -------
        [HttpPost("CreateDetails")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateDetails([FromBody] CreateOfficeTypeXConfigureDetailsCommand request)
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

        // ------- Master Get All -------
        [HttpGet("GetAllMaster")]
        [ProducesResponseType(typeof(List<OfficeTypeXConfigMasterVM>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllMaster()
        {
            var result = await _mediator.Send(new GetMasterAllQuery());
            return CustomResult(result);
        }

        // ------- Details Get All -------
        [HttpGet("GetAllDetails")]
        [ProducesResponseType(typeof(List<OfficeTypeXConfigureDetailsVM>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllDetails()
        {
            var result = await _mediator.Send(new GetDetailsAllQuery());
            return CustomResult(result);
        }

        // ------- Get Master by Id -------
        [HttpGet("GetMasterById")]
        [ProducesResponseType(typeof(OfficeTypeXConfigMasterVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMasterById([FromQuery] int id)
        {
            var result = await _mediator.Send(new GetMasterByIdQuery(id));
            return CustomResult(result);
        }

        // ------- Get Details by Id -------
        [HttpGet("GetDetailsById")]
        [ProducesResponseType(typeof(OfficeTypeXConfigureDetailsVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDetailsById([FromQuery] int id)
        {
            var result = await _mediator.Send(new GetDetailsByIdQuery(id));
            return CustomResult(result);
        }

    }
}
