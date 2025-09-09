using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility.CommonController;
using Utility.Domain;
using Utility.Enums;
using Utility.Enums.HRM;

namespace Global.API.Controllers
{
    public class CommonDropDownController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadBloodGroup(string value = "")
        {
            var lst = new BloodGroup().GetBloodGroupDropDown(value);

            return CustomResult(lst);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadDays(string value = "")
        {
            var lst = new Days().GetDaysDropDown(value);
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
        public IActionResult LoadGeoLocationType(string value = "")
        {
            var lst = new GeoLocationType().GetGeoLocationTypeDropDown(value);
            return CustomResult(lst);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadOfficeType(string value = "")
        {
            string[] arr = { "1", "2" };
            var lst = new OfficeType().GetOfficeTypeDropDown(value).Where(x => !arr.Contains(x.Value));
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
        public IActionResult LoadReligion(string value = "")
        {
            var lst = new Religion().GetReligionDropDown(value);
            return CustomResult(lst);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadSpouseType(string value = "")
        {
            var lst = new SpouseType().GetSpouseTypeDropDown(value);
            return CustomResult(lst);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadBankType(string value = "")
        {
            var lst = new BankType().GetBankTypeDropDown(value);
            return CustomResult(lst);
        }
       

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadEmployeeType(string value = "")
        {
            var lst = new EmployeeType().GetEmployeeTypeDropDown(value);
            return CustomResult(lst);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadEmployeeStatus(string value = "")
        {
            var lst = new EmployeeStatus().GetEmployeeStatusDropDown(value);
            return CustomResult(lst);
        }

        
    }
}
