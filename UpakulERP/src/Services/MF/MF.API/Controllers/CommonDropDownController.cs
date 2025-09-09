using System.Net;
using MediatR;
using MF.Application.Contacts.Enums;
using MF.Application.Features.DBOrders.Queries.GeoLocation;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using Utility.Domain;
using Utility.Enums;
using static Utility.Enums.Days;

namespace MF.API.Controllers
{
    public class CommonDropDownController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public CommonDropDownController(IMediator mediator)
        {
            _mediator = mediator;
        }

      
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadComponent(string value = "")
        {
            var lst = new Component().GetComponentTypeDropDown(value);
            return CustomResult(lst);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadGender(string value = "")
        {
            var lst = new Gender().GetGenderDropDown(value);
            return CustomResult(lst);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadMaritalStatus(string value = "", bool? allowSingle = true)
        {
            var lst = new MaritalStatus().GetMaritalStatusDropDown(value: value, allowSingle: allowSingle ?? true);
            return CustomResult(lst);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadPeriodicPayment(string value = "")
        {
            var lst = new PeriodicPayment().GetPeriodicPaymentDropDown(value);
            return CustomResult(lst);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadDisbursementRequestActivities(string value = "")
        {
            var lst = new DisbursementRequestActivities().GetDisbursementRequestActivitiesDropDown(value);
            return CustomResult(lst);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadMemberRemarks(string value = "")
        {
            var lst = new MemberRemarks().GetMemberRemarksDropDown(value);
            return CustomResult(lst);
        }

        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        //public IActionResult LoadPaymentMethod(string value = "")
        //{
        //    var lst = new PaymentMethod().GetPaymentMethodDropDown(value);
        //    return CustomResult(lst);
        //}

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadPaymentType(string value = "")
        {
            var lst = new PaymentType().GetPaymentTypeDropDown(value);
            return CustomResult(lst);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadMemberEducation(string value = "")
        {
            var lst = new MemberEducation().GetMemberEducationDropDown(value);
            return CustomResult(lst);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadGroupType(string value = "")
        {
            var lst = new GroupType().GetGroupTypeDropDown(value);
            return CustomResult(lst);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadDays(string value = "")
        {
            string[] arr = { DaysEnum.Friday.ToString(), DaysEnum.Saturday.ToString() };
            var lst = new Days().GetDaysDropDown(value).Where(x => !arr.Contains(x.Text));
            return CustomResult(lst);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadOfficeType(string value = "")
        {
            var lst = new OfficeType().GetOfficeTypeDropDown(value);
            return CustomResult(lst);
        }

        /// <summary>
        /// Geo LOcation
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGeoLocationByParentId(int? parentId)
        {
            try
            {
                var lstObj = await _mediator.Send(new GeoLocationDropdownQuery(parentId));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        //public IActionResult LoadGroupCommitteePositions()
        //{
        //    var lst = new GroupCommittee().GetGroupCommitteeDropDown();
        //    return CustomResult(lst);
        //}
    }
}
