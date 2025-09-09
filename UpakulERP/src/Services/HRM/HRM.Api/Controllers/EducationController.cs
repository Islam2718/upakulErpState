using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using HRM.Application.Features.DBOrders.Commands.Delete.Commands;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;
using HRM.Application.Features.DBOrders.Queries.BoardUniversity;
using HRM.Application.Features.DBOrders.Queries.Education;
using HRM.Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility.CommonController;
using Utility.Domain;

namespace HRM.Api.Controllers
{
    public class EducationController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public EducationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateEducationCommand request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.EducationName))
                    return CustomResult("Education Name required.", HttpStatusCode.BadRequest);
                else
                {
                    request.CreatedBy = loggedInEmployeeId;
                    var response = await _mediator.Send(request);
                    return CustomResult(response.Message, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(EducationVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new GetEducationByIdQuery(id));
            return CustomResult(obj);
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateEducationCommand request)
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
        public async Task<IActionResult> Delete([FromBody] DeleteEducationCommand request)
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
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EducationDropdown()
        {
            var lst = await _mediator.Send(new EducationDropdownQuery());
            return CustomResult(lst);
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
            var query = new EducationGirdQuery
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
