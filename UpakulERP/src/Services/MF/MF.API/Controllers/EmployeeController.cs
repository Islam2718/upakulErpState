using System.Net;
using MediatR;
using MF.Application.Features.DBOrders.Queries.Designation;
using MF.Application.Features.DBOrders.Queries.Employee;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using Utility.Domain;

namespace MF.API.Controllers
{
    public class EmployeeController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetEmployeeforDropdown()
        {
            try
            {
                var lstObj = await _mediator.Send(new EmployeeDropdownQuery(employeeId: 0, officeId: loggedInOfficeId));
                return CustomResult(lstObj, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        // Designation
        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DesignationDropdown()
        {
            var lst = await _mediator.Send(new DesignationDropdownQuery(loggedInOfficeId??0));
            return CustomResult(lst);
        }


    }
}
