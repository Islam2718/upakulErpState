using MediatR;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Domain.ViewModels;
using Utility.Domain;
using MF.Application.Features.DBOrders.Queries.Group;

namespace MF.API.Controllers
{
    public class GroupController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateGroupCommand request)
        {
            try
            {
                    request.CreatedBy = loggedInEmployeeId;
                    request.CreatedOn = DateTime.Now;
                    var response = await _mediator.Send(request);
                    return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(SamityVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new GroupByIdQuery(id));
            return CustomResult(obj);
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateGroupCommand request)
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
        public async Task<IActionResult> Delete([FromBody] DeleteGroupCommand request)
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
            int? OfficeId = loggedInOfficeId.Value;
            var query = new GroupGridQuery
            {
                OfficeId = OfficeId,
                Page = page,
                PageSize = pageSize,
                Search = search,
                SortOrder = SortOrder,
            };

            var result = await _mediator.Send(query);
            return Ok(result);
       }

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGroupDropdown(int? id)
        {
            try
            {
                int? OfficeId = loggedInOfficeId.Value;
                var lstObj = await _mediator.Send(new GroupDropdownQuery(id ?? 0, OfficeId));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetEmployeeXGroupDropdown(int? empId)
        {
            try
            {
                var listObj = await _mediator.Send(new EmployeeXGroupDropdownQuery(empId ?? 0));
                //return CustomResult(listObj);
                return Ok(listObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
