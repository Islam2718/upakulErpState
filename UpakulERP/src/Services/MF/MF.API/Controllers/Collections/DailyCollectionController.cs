using MediatR;
using MF.Application.Features.DBOrders.Queries.Component;
using MF.Domain.ViewModels.Collection;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility.CommonController;

namespace MF.API.Controllers.Collections
{
    public class DailyCollectionController :ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public DailyCollectionController(IMediator mediator)
        {
           _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(EmployeeXGroupCollectionVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetEmployeeXGroupSheet()
        {
            if (loggedInOfficeTypeId == 6 && !string.IsNullOrEmpty(LoggedInTransactionDate))
            {
                var response = await _mediator.Send(new EmployeeXGroupCollectionQuery((loggedInOfficeId ?? 0), (loggedInEmployeeId ?? 0), DateTime.Parse(LoggedInTransactionDate)));
                return Ok(response);
            }
            else
                return Ok(new List<EmployeeXGroupCollectionVM>());
        }

        [HttpGet]
        [ProducesResponseType(typeof(GroupXMemberCollectionVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGroupXMemberSheet(int groupId)
        {
            var response = await _mediator.Send(new GroupXMemberCollectionQuery((loggedInOfficeId ?? 0), (loggedInEmployeeId ?? 0), DateTime.Parse(LoggedInTransactionDate), groupId));
            return Ok(response);
        }
    }
}
