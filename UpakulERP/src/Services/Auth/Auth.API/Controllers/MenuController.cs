using System.Net;
using Auth.API.DTO.Request;
using Auth.API.DTO.Response;
using Auth.API.Models;
using Auth.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using Utility.Constants;
using Utility.Domain;

namespace Auth.API.Controllers
{
    public class MenuController : ApiController
    {
        IMenuStrategy _menuStrategy;
        IRoleXMenuStrategy _roleXMenuStrategy;
        public MenuController(IMenuStrategy menuStrategy, IRoleXMenuStrategy roleXMenuStrategy)
        {
            _menuStrategy = menuStrategy;
            _roleXMenuStrategy = roleXMenuStrategy;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMenubyModuleDropdown(int moduleId)
        {
            try
            {
                var lst = _menuStrategy.GetMenubyModule(moduleId);
                return CustomResult(lst, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MenuPermissionDTOResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMenuPermission(int moduleId, int roleId)
        {
            try
            {
                var lst = _menuStrategy.GetMenuPermission(moduleId, roleId);
                return CustomResult(lst, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create(UserMenu request)
        {
            try
            {
                var lst = _menuStrategy.CreateMenu(request);
                return CustomResult(lst, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RoleXMenuCreate([FromBody] List<MenuPermissionRequestCommand> request)
        {
            if (request == null)
                return CustomResult(MessageTexts.data_not_found, HttpStatusCode.NotFound);
            else if (!request.Any())
                return CustomResult(MessageTexts.data_not_found, HttpStatusCode.NotFound);
            else if (request.Where(x => x.ModuleId == 0).Any())
                return CustomResult("Module " + MessageTexts.data_not_found, HttpStatusCode.NotFound);
            else if (request.Where(x => x.RoleId == 0).Any())
                return CustomResult("Role " + MessageTexts.data_not_found, HttpStatusCode.NotFound);
            else if (request.Where(x => x.MenuId == 0).Any())
                return CustomResult("Menu " + MessageTexts.data_not_found, HttpStatusCode.NotFound);
            else
            {
                var response = await _roleXMenuStrategy.Create(request, loggedInEmployeeId ?? 0);
                return CustomResult(response.Message, response.StatusCode);
            }
        }

        

    }
}
