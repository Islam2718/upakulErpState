using Accounts.Application.Features.DBOrders.Queries.BudgetComponent;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using MediatR;
using Accounts.Domain.ViewModel;
using Accounts.Application.Features.DBOrders.Queries.BudgetEntry;

namespace Accounts.API.Controllers.Budget
{
    public class BudgetEntryController : ApiController
    {

        #region Var
        IMediator _mediator;
        #endregion Var
        public BudgetEntryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BudgetEntryComponentVM>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBudgetEntryComponent(string financialYear, int officeId, int componentParentId)   //, int ComponentId
        {
            try
            {
                var lstObj = await _mediator.Send(new GetRowQuery(financialYear, officeId, componentParentId));  //, ComponentId
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        

    }
}
