using Auth.API.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using Auth.API.Repositories.Interfaces;
using Utility.Domain;
using Auth.API.DTO.Request;

namespace Auth.API.Controllers
{
    public class RoleController : ApiController
    {
        #region Variables
        private IRoleRepository _roleRepository;
        #endregion Variables

        public RoleController(IRoleRepository repository)
        {
            _roleRepository = repository;            
        }

        #region Create Role
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateRoleDtoRequest request)
        {
            try
            {              
              CreateRoleDtoRequest _recordObj = new(Name: (!string.IsNullOrEmpty(request.Name) ? request.Name : ""), ModuleId: request.ModuleId);
              
              var result = await _roleRepository.CreateRoleAsync(_recordObj);
              return CustomResult(result.Message, result.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        #endregion end Create Role


        #region Get Role
        

        [HttpGet]
        public async Task<IActionResult> LoadList([FromQuery] int moduleId)
        {
            try
            {
                return CustomResult(await _roleRepository.LoadGrid(moduleId), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRoleByModuleIdDropdown([FromQuery] int moduleId)
        {
            try
            {
                return CustomResult(_roleRepository.GetRoleByModuleIdDropdown(moduleId), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApplicationRole), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                return CustomResult(await _roleRepository.GetByRoleId(id), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        #endregion end Get Role

        #region deleteRole
        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var roleId = id.ToString();
            try
            {
                var result = await _roleRepository.DeleteRoleAsync(roleId);
                return CustomResult(result.Message, result.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        #endregion 

        #region Update Role

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateRoleDtoRequest request)
        {
            try
            {
                var result = await _roleRepository.UpdateRoleAsync(request);                    
                return CustomResult(result.Message, result.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        #endregion end Update Role


    }
}
