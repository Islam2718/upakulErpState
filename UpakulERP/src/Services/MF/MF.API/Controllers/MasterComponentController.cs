using MediatR;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Application.Features.DBOrders.Queries.MasterComponent;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility.CommonController;
using Utility.Domain;

namespace MF.API.Controllers
{
    public class MasterComponentController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public MasterComponentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateMasterComponentCommand request)
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
        //[HttpGet]
        ////[AllowAnonymous]
        //[ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> GetMasterComponents(int? id)
        //{
        //    try
        //    {
        //        var lstObj = await _mediator.Send(new MasterComponentQuery(id));

        //        return CustomResult(lstObj);
        //    }
        //    catch (Exception ex)
        //    {
        //        return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
        //    }
        //}


        [HttpGet]
        [ProducesResponseType(typeof(MasterComponentVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new MasterComponentByIdQuery(id));
            return CustomResult(obj);
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateMasterComponentCommand request)
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
        public async Task<IActionResult> Delete([FromBody] DeleteMasterComponentCommand request)
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
           // Console.WriteLine($"Received Parameters -> Page: {page}, PageSize: {pageSize}, Search: '{search}', SortColumn: '{sortColumn}', SortDirection: '{sortDirection}'");
            var query = new MasterComponentGridQuery
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
                SortOrder = SortOrder,
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        //Get Bank For DropDown
        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMasterComponentForDropdown(int? id)
        {
            try
            {
                var list = await _mediator.Send(new MasterComponentDropdownQuery(id ?? 0));
                return CustomResult(list);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

