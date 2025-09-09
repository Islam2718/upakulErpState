using MediatR;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using MF.Application.Features.DBOrders.Queries.OfficeComponentMapping;
using MF.Domain.Models;

namespace MF.API.Controllers
{
    public class OfficeComponentMappingController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public OfficeComponentMappingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateOfficeComponentMappingCommand request)
        {
            try
            {
                request.loggedInEmployeeId = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(OfficeComponentMapping), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByComponentId(int id)
        {
            var obj = await _mediator.Send(new GetDataByIdQuery(id));
            return CustomResult(obj);
        }
    }
}
