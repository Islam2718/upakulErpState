using System.Net;
using MediatR;
using MF.Application.Features.DBOrders.Queries.Office;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using Utility.Domain;
using Utility.Enums;

namespace MF.API.Controllers
{
    public class OfficeController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public OfficeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region For Office Drop Down
        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOfficeByParentId(int? parentId)
        {
            try
            {
                var lstObj = await _mediator.Send(new OfficeByPIDDropdownQuery(parentId));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBranchOfficeDropdown()
        {
            try
            {
                var lstObj = await _mediator.Send(new OfficeDropdownQuery(loggedInOfficeId ?? 0, (int)OfficeType.OfficeTypeEnum.Branch));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet, ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOfficeDropdown()
        {
            try
            {
                var lstObj = await _mediator.Send(new OfficeDropdownQuery(loggedInOfficeId ?? 0));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        #endregion For Office Drop Down
    }
}
