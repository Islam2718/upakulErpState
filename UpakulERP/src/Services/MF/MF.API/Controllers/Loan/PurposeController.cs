using MediatR;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Application.Features.DBOrders.Queries.MainPurpose;
using MF.Domain.Models.View;
using MF.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility.CommonController;
using Utility.Domain;

namespace MF.API.Controllers.Loan
{
    public class PurposeController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public PurposeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreatePurposeCommand request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name))
                    return CustomResult("Purpose name is required.", HttpStatusCode.BadRequest);
                else
                {
                    request.CreatedBy = loggedInEmployeeId;
                    var response = await _mediator.Send(request);
                    return CustomResult(response.Message, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdatePurposeCommand request)
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
        public async Task<IActionResult> Delete([FromBody] DeletePurposeCommand request)
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
        [HttpPost]
        public async Task<IActionResult> GetPurposes(PurposeGridQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(VwPurpose), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new GetPurposeByIdQuery(id));
            return CustomResult(obj);
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMainPurposeDropdown([FromQuery] int? pId = null)
        {
            var lstObj = await _mediator.Send(new MainPurposeDropdownQuery(pId ?? 0));
            return CustomResult(lstObj);
        }
            /* Presently no need 
             * Closs by Mahfuz
            [HttpGet]
            [ProducesResponseType(typeof(List<CustomSelectListItem>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetMainPurposeDropdown([FromQuery] string searchText, int? id = null)
            {
                try
                {
                    var lstObj = await _mediator.Send(new MainPurposeDropdownQuery(id ?? 0, searchText));
                    return CustomResult(lstObj);
                }
                catch (Exception ex)
                {
                    return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
                }
            }
            */
        }
}
