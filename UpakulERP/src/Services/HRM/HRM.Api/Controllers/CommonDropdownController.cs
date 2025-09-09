using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility.Domain;
using MediatR;
using HRM.Application.Features.DBOrders.Queries.Department;
using Utility.CommonController;
using Utility.Enums.HRM;
using Utility.Enums;

namespace HRM.Api.Controllers
{
    public class CommonDropdownController : ApiController
    {
        private readonly IMediator _mediator;

        public CommonDropdownController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Dropdown APIs

        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        //public IActionResult LeaveType()
        //{
        //    var list = new List<CustomSelectListItem>
        //    {
        //        new() { Value = "1", Text = "Sick Leave" },
        //        new() { Value = "2", Text = "Casual Leave" },
        //        new() { Value = "3", Text = "Earned Leave" },
        //        new() { Value = "4", Text = "Maternity Leave" },
        //        new() { Value = "5", Text = "Paternity Leave" },
        //        new() { Value = "6", Text = "Leave Without Pay" },
        //        new() { Value = "7", Text = "Study Leave" },
        //        new() { Value = "8", Text = "Marriage Leave" },
        //        new() { Value = "9", Text = "Bereavement Leave" },
        //        new() { Value = "10", Text = "Compensatory Leave" }
        //    };
        //    return Ok(list);
        //}

        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        //public IActionResult EmployeeType()
        //{
        //    var list = new List<CustomSelectListItem>
        //    {
        //        new() { Value = "1", Text = "Permanent" },
        //        new() { Value = "2", Text = "Contractual" },
        //        new() { Value = "3", Text = "Part-Time" },
        //        new() { Value = "4", Text = "Intern" },
        //        new() { Value = "5", Text = "Temporary" }
        //    };
        //    return Ok(list);
        //}

        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        //public IActionResult Gender()
        //{
        //    var list = new List<CustomSelectListItem>
        //    {
        //        new() { Value = "M", Text = "Male" },
        //        new() { Value = "F", Text = "Female" }
        //    };
        //    return Ok(list);
        //}

        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        //public IActionResult EligibleFrom()
        //{
        //    var list = new List<CustomSelectListItem>
        //    {
        //        new() { Value = "1", Text = "ED" },
        //        new() { Value = "2", Text = "DED" },
        //        new() { Value = "3", Text = "DD" },
        //        new() { Value = "4", Text = "Supervisor" }
        //    };
        //    return Ok(list);
        //}

        //[HttpGet("LoadDepartmentDropdown")]
        //[ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> LoadDepartmentDropdown()
        //{
        //    var list = await _mediator.Send(new DepartmentDropdownQuery());
        //    return Ok(list);
        //}

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadLeaveCategory(string value = "")
        {
            var lst = new LeaveCategory().GetLeaveCategoryDropDown(value);
            return Ok(lst);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadEmployeeType(string value = "")
        {
            var lst = new EmployeeType().GetEmployeeTypeDropDown(value);
            return Ok(lst);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadGender(string value = "")
        {
            var lst = new Gender().GetGenderDropDown(value);
            return Ok(lst);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadOfficeType(string value = "")
        {
            var lst = new OfficeType().GetOfficeTypeDropDown(value);
            return CustomResult(lst);
        }

        #endregion
    }
}