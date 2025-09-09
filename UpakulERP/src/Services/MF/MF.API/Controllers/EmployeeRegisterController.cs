using System.Net;
using MediatR;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Application.Features.DBOrders.Queries.EmployeeRegister;
using MF.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using Utility.Domain;
using Utility.Enums;

namespace MF.API.Controllers
{
    public class EmployeeRegisterController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public EmployeeRegisterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGroupByEmployeeIdDropdown(int? employeeId)
        {
            try
            {
                var lstObj = await _mediator.Send(new GroupByEmployeeDropdownQuery(employeeId ?? 0));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateGroupWiseEmployeeAssignCommand request)
        {
            try
            {
                request.loginEmployeeId = loggedInEmployeeId;
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
        public async Task<IActionResult> Release([FromBody] UpdateGroupWiseEmployeeAssignCommand request)
        {
            try
            {
                DateTime releaseDate = DateTime.Now;
                DateTime.TryParse(LoggedInTransactionDate?? DateTime.Now.ToString("dd-MMM-yyyy"),out releaseDate);
                request.UpdatedBy = loggedInEmployeeId;
                request.ReleaseDate = releaseDate;
                var response = await _mediator.Send(request);
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(MultipleDropdownForGrpWiseEmployeeVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllGrpWiseEmployeeDropDownData()
        {
            var obj = await _mediator.Send(new GrpWiseEmployeeDropdownQuery(loggedInOfficeId ?? 0, loggedInOfficeTypeId ?? 0));
            return CustomResult(obj);
        }

        [HttpGet]
        public async Task<IActionResult> LoadGrid(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 0,
        [FromQuery] string search = "",
        [FromQuery] string SortOrder = "")
        {
            var query = new EmployeeRegisterGridQuery
            {
                logedInOfficeId = loggedInOfficeId,
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
