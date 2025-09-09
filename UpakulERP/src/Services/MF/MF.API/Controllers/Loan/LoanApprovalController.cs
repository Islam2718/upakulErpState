using System.Net;
using MediatR;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Application.Features.DBOrders.Queries.LoanApproval;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;

namespace MF.API.Controllers.Loan
{
    public class LoanApprovalController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public LoanApprovalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllQuery());
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] List<CreateLoanApprovalCommand> request)
        {
            try
            {
                var response = await _mediator.Send(new CreateLoanApprovalListCommand(request));
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromBody] DeleteLoanApprovalCommand request)
        {
            try
            {
                var response = await _mediator.Send(request);
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


    }
}
