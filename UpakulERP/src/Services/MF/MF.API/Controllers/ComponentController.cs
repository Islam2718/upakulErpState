using MediatR;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Application.Features.DBOrders.Queries.Component;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility.CommonController;
using Utility.Domain;
using MF.Application.Features.DBOrders.Queries.ComponentSetup;
using MF.Application.Contacts.Enums;


namespace MF.API.Controllers
{
    public class ComponentController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public ComponentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateComponentCommand request)
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
        [ProducesResponseType(typeof(Domain.Models.Component), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new ComponentByIdQuery(id));
            return CustomResult(obj);
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateComponentCommand request)
        {
            try
            {
                request.UpdatedBy = loggedInEmployeeId;
                request.UpdatedOn = DateTime.Now;
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
        public async Task<IActionResult> Delete([FromBody] DeleteComponentCommand request)
        {
            try
            {
                request.DeletedBy = loggedInEmployeeId;
                request.DeletedOn = DateTime.Now;
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
            // ✅ Debugging log
            //Console.WriteLine($"Received Parameters -> Page: {page}, PageSize: {pageSize}, Search: '{search}', SortColumn: '{sortColumn}', SortDirection: '{sortDirection}'");
            var query = new ComponentGridQuery
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
                SortOrder = SortOrder,
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        #region Drop down
        #region Loan
        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLoanComponentDropdown([FromQuery] int? id)
        {
            try
            {
                var lstObj = await _mediator.Send(new ComponentDropdownQuery((id ?? 0), (loggedInOfficeId ?? 0), Component.Loan, null));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGeneralLoanComponentDropdown([FromQuery] int? id)
        {
            try
            {
                var lstObj = await _mediator.Send(new ComponentDropdownQuery((id ?? 0), (loggedInOfficeId ?? 0), Component.Loan, "G"));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProjectLoanComponentDropdown([FromQuery] int? id)
        {
            try
            {
                var lstObj = await _mediator.Send(new ComponentDropdownQuery((id ?? 0), (loggedInOfficeId ?? 0), Component.Loan, "P"));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        #endregion Loan 

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSavingComponentDropdown([FromQuery] int? id)
        {
            try
            {
                var lstObj = await _mediator.Send(new ComponentDropdownQuery((id ?? 0), (loggedInOfficeId ?? 0), "S", null));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDPSComponentDropdown([FromQuery] int? id)
        {
            try
            {
                var lstObj = await _mediator.Send(new ComponentDropdownQuery((id ?? 0), (loggedInOfficeId ?? 0), Component.Term_Savings_DPS, null));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFDRComponentDropdown([FromQuery] int? id)
        {
            try
            {
                var lstObj = await _mediator.Send(new ComponentDropdownQuery((id ?? 0), (loggedInOfficeId ?? 0), Component.Term_Savings_FDR, null));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        #endregion
    }
}

