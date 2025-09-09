using MediatR;
using MF.Application.Features.DBOrders.Queries.MRAPurpose;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility.CommonController;
using Utility.Domain;

namespace MF.API.Controllers.Loan
{
    public class MRAPurposeController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public MRAPurposeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Get MRAPurpose For DropDown
        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMRAPurposeDropdown(string category,string? subcategory)
        {
            try
            {
                var lstObj = await _mediator.Send(new MRAPurposeDropdownQuery(category, subcategory));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

    }
}
