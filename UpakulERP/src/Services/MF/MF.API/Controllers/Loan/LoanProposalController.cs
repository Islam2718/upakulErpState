using System.Net;
using MediatR;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Application.Features.DBOrders.Queries.LoanProposal;
using MF.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using Utility.Domain;

namespace MF.API.Controllers.Loan
{
    public class LoanProposalController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public LoanProposalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromForm] CreateLoanApplicationCommand request)
        {
            try
            {
                request.CreatedBy = loggedInEmployeeId;
                request.OfficeId = loggedInOfficeId.Value;
                var response = await _mediator.Send(request);
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromForm] UpdateLoanApplicationCommand request)
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
        public async Task<IActionResult> Delete([FromBody] DeleteLoanProposalCommand request)
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
        [ProducesResponseType(typeof(LoadGridForLoanApproveVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new LoanProposalByIdQuery(id));
            return CustomResult(obj);
        }

        [HttpGet]
        [HttpGet]
        [ProducesResponseType(typeof(LoanFormVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLoanForm(long loanApplicationId,long? summaryId)
        {
            var obj = await _mediator.Send(new LoanFormQuery(loanApplicationId, (summaryId??0)));
            return CustomResult(obj);
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoanApprovalFlow([FromBody] UpdateLoanApprovalFlowCommand request)
        {
            try
            {
                request.loggedInEmpId = loggedInEmployeeId;
                request.loggedInOfficeId = loggedInOfficeId;
                request.loggedInOfficeTypeId = loggedInOfficeTypeId;
                if (!string.IsNullOrEmpty(LoggedInTransactionDate))
                    request.transactionDate = DateTime.Parse(LoggedInTransactionDate);
                var response = await _mediator.Send(request);
                return CustomResult(response.Message,response.Returnobj ,response.StatusCode);
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
            var query = new LoanProposalGridQuery
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
                SortOrder = SortOrder,
                logedInOfficeId= loggedInOfficeId.Value
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        //public async Task<IActionResult> LoadGridForLoanApprove(
        //[FromQuery] int page = 1,
        //[FromQuery] int pageSize = 0,
        //[FromQuery] string search = "",
        //[FromQuery] string SortOrder = "")
        //{
        //    var query = new LoanProposalGridQuery
        //    {
        //        Page = page,
        //        PageSize = pageSize,
        //        Search = search,
        //        SortOrder = SortOrder,
        //        loggedInEmployeeId = loggedInEmployeeId.Value,
        //        logedInOfficeId = loggedInOfficeId.Value,
        //        loggedInOfficeTypeId = loggedInOfficeTypeId.Value,
        //    };

        //    var result = await _mediator.Send(query);
        //    return Ok(result);
        //}

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoanProposalForDropdown(int? id)
        {
            try
            {
                var lstObj = await _mediator.Send(new LoanProposalQuery(id ?? 0));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

    }
}
