using MediatR;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using Project.Application.Features.DBOrders.Commands.Create.Commands;
using Utility.Domain;
using Project.Application.Features.DBOrders.Queries.Doner;
using Project.Application.Features.DBOrders.Commands.Update.Commands;
using Project.Application.Features.DBOrders.Commands.Delete.Commands;
using roject.Domain.ViewModels;

namespace Project.API.Controllers
{
    public class DonerController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public DonerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDonerCommand request)
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
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        //[AllowAnonymous]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDonerDropdown()
        {
            try
            {
                var lstObj = await _mediator.Send(new DonerDropdownQuery());

                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(DonerVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new DonerByIdQuery(id));
            return CustomResult(obj);
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDonerCommand request)
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
        public async Task<IActionResult> Delete([FromBody] DeleteDonerCommand request)
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
            var query = new DonerGridQuery
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
