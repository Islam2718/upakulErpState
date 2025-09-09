using System.Net;
using Accounts.Application.Features.DBOrders.Commands.Create.Commands;
using Accounts.Application.Features.DBOrders.Commands.Delete.Command;
using Accounts.Application.Features.DBOrders.Commands.Update.Command;
using Accounts.Application.Features.DBOrders.Queries.BudgetComponent;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using Utility.Domain;

namespace Accounts.API.Controllers.Budget
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
        public async Task<IActionResult> Create([FromBody] CreateBudgetComponentCommand request)
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
        [ProducesResponseType(typeof(BudgetComponentVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new GetByIdQuery(id));
            return CustomResult(obj);
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateBudgetComponentCommand request)
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
        public async Task<IActionResult> Delete([FromBody] DeleteBudgetComponentCommand request)
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
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetComponentForDropdown(int? parentId)
        {
            try
            {
                var lstObj = await _mediator.Send(new GetQuery(parentId));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BudgetComponentVM>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetComponentListByParentId(int? parentId)
        {
            try
            {
                var lstObj = await _mediator.Send(new GetListByParentIdQuery(parentId));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        //[HttpGet]
        //[ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> GetParentComponentForDropdown(int? parentId)
        //{
        //    try
        //    {
        //        var lstObj = await _mediator.Send(new GetQuery(parentId));
        //        return CustomResult(lstObj);
        //    }
        //    catch (Exception ex)
        //    {
        //        return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
        //    }
        //}


        //[HttpGet]
        //public async Task<IActionResult> LoadGrid(
        //[FromQuery] int page = 1,
        //[FromQuery] int pageSize = 0,
        //[FromQuery] string search = "",
        //[FromQuery] string sortColumn = "",
        //[FromQuery] string sortDirection = "")
        //{
        //    // ✅ Debugging log
        //    //Console.WriteLine($"Received Parameters -> Page: {page}, PageSize: {pageSize}, Search: '{search}', SortColumn: '{sortColumn}', SortDirection: '{sortDirection}'");
        //    var query = new GetListQuery
        //    {
        //        Page = page,
        //        PageSize = pageSize,
        //        Search = search,
        //        SortColumn = sortColumn,
        //        SortDirection = sortDirection
        //    };

        //    var result = await _mediator.Send(query);
        //    return Ok(result);
        //}


    }


}
