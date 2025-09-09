using System.Net;
using Auth.API.DTO.Response;
using Auth.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using Utility.Domain;
using Utility.Enums;

namespace Auth.API.Controllers
{
    public class ModuleController : ApiController
    {
        private IModuleStrategy _moduleStrategy;
        public ModuleController(IModuleStrategy moduleStrategy)
        {
            _moduleStrategy = moduleStrategy;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetModuleByDropdown()
        {
            try
            {
                return CustomResult(_moduleStrategy.GetAllforDropdown(), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserXModuleDTOResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserXModule(int employeeid)
        {
            try
            {
                return CustomResult(_moduleStrategy.GetUserXModule(employeeid), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

    }
}
