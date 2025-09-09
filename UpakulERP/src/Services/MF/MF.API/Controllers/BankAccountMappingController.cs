using MediatR;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Application.Features.DBOrders.Queries.BankAccountMapping;
using MF.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility.CommonController;
using Utility.Domain;

namespace MF.API.Controllers
{
    public class BankAccountMappingController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public BankAccountMappingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateBankAccountMappingCommand request)
        {
            try
            {
                request.OfficeId = loggedInOfficeId.Value;
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
        [ProducesResponseType(typeof(BankAccountMappingVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new BankAccountMappingByIdQuery(id));
            return CustomResult(obj);
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateBankAccountMappingCommand request)
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
        public async Task<IActionResult> Delete([FromBody] DeleteBankAccountMappingCommand request)
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
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetChequeDetailsDropdown(int id)
        {
            try
            {
                var lstObj = await _mediator.Send(new BankAccountChequeDropDownQuery(id));
                return CustomResult(lstObj, HttpStatusCode.OK);
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
            var query = new BankAccountMappingGridQuery
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
                SortOrder = SortOrder,
                logedInOfficeId = loggedInOfficeId??0
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> LoadChequeDetailsGrid(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 0,
        [FromQuery] string search = "",
        [FromQuery] string SortOrder = "",
        int? mappingId = 0)
        {
            var query = new BankAccountChequeDetailsGridQuery
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
                SortOrder = SortOrder,
                BankAccountMappingId = mappingId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }


        ///Cheque && ChequeDetails
        ///
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateCheque([FromBody] CreateBankAccountChequeCommand request)
        {
            try
            {
               // request.OfficeId = loggedInOfficeId.Value;
                request.CreatedBy = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        #region Drop down
        [HttpGet]
        [ProducesResponseType(typeof(OfficeBankAssignDropdownVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOfficeBankAssignDropdownData()
        {
            var obj = await _mediator.Send(new OfficeBankAssignDropdownDataQuery(loggedInOfficeId ?? 0));
            return CustomResult(obj);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOfficeXBankDropdown()
        {
            var obj = await _mediator.Send(new OfficeXBankDropdownQuery(loggedInOfficeId ?? 0));
            return CustomResult(obj);
        }
        #endregion

    }
}
