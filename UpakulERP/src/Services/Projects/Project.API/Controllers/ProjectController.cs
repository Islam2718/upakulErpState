using MediatR;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.DBOrders.Commands.Create.Commands;
using Project.Application.Features.DBOrders.Commands.Delete.Commands;
using Project.Application.Features.DBOrders.Commands.Update.Commands;
using Project.Application.Features.DBOrders.Queries.Project;
using Utility.CommonController;
using Utility.Domain;
using roject.Domain.ViewModels;
using Project.Domain.ViewModels;

namespace Project.API.Controllers
{
    public class ProjectController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateProjectCommand request)
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
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProjectDropDown()
        {
            try
            {
                var lstObj = await _mediator.Send(new ProjectDropdownQuery());
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProjectVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new ProjectByIdQuery(id));
            return CustomResult(obj);
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateProjectCommand request)
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
        public async Task<IActionResult> Delete([FromBody] DeleteProjectCommand request)
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
            var query = new ProjectGridQuery
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
