using Auth.API.DTO.Request;
using Auth.API.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Utility.Constants;
using Utility.CommonController;
using Auth.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Utility.Domain;

namespace Auth.API.Controllers
{
    public class UserController : ApiController
    {
        #region Variables
        private IUserStrategy _userStrategy;
        private IEmployeeStrategy _employeeStrategy;
        private IRoleXModuleStrategy _roleXModuleStrategy;

        private readonly UserManager<ApplicationUser> _userManager;        


        #endregion Variables
        public UserController(IUserStrategy userStrategy, IEmployeeStrategy employeeStrategy, IRoleXModuleStrategy roleXModuleStrategy, UserManager<ApplicationUser> UserManager)
        {
            _userStrategy = userStrategy;
            _employeeStrategy = employeeStrategy;
            _roleXModuleStrategy = roleXModuleStrategy;
            _userManager = UserManager;
        }

        #region Create User
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Register([FromBody] RegisterDtoRequest request)
        {
            try
            {
                if (request.rolesXModules == null)
                    return CustomResult("Module " + MessageTexts.data_not_found, HttpStatusCode.BadRequest);
                else if (request.rolesXModules.Where(x => x.ModuleId == 0).Any()) return CustomResult("Module info. " + MessageTexts.data_not_found, HttpStatusCode.BadRequest);
                else if (request.rolesXModules.Where(x => x.RoleId == 0).Any()) return CustomResult("Role info. " + MessageTexts.data_not_found, HttpStatusCode.BadRequest);


                var emp = await _employeeStrategy.FindByEmpId(request.EmployeeId);
                if (emp != null)
                {
                    string msg = ""; HttpStatusCode statusCode = HttpStatusCode.OK;
                    if ((request.UserId ?? 0) == 0)
                    {
                        #region Registration
                        UserDtoRequest _recordObj = new(EmployeeId: emp.EmployeeId, UserName: request.UserName
                            , Email: (!string.IsNullOrEmpty(emp.OfficialEmail) ? emp.OfficialEmail : !string.IsNullOrEmpty(emp.PersonalEmail) ? emp.PersonalEmail : "")
                            , FirstName: emp.FirstName, LastName: emp.LastName??"", Password: request.Password, ConfirmPassword: request.ConfirmPassword, loggedInEmployeeId ?? 0);

                        var user_result = await _userStrategy.CreateUserAsync(_recordObj);
                        if (user_result.StatusCode != HttpStatusCode.Created)
                            return CustomResult(user_result.Message, user_result.StatusCode);
                        else
                            request.UserId = user_result.ReturnId;
                        #endregion Registration
                    }
                    else
                    {
                        var user = await _userStrategy.GetById(request.UserId ?? 0);
                        if (user == null)
                            return CustomResult("User " + MessageTexts.data_not_found, HttpStatusCode.BadRequest);
                    }
                    List<RoleXModule> lst = new List<RoleXModule>();
                    foreach (var item in request.rolesXModules)
                    {
                        RoleXModule roleXModule = new RoleXModule
                        {
                            CreatedBy = loggedInEmployeeId,
                            ModuleId = item.ModuleId,
                            RoleId = item.RoleId,
                            UserId = request.UserId.Value
                        };
                        lst.Add(roleXModule);
                    }
                    var result = await _roleXModuleStrategy.Create(lst);

                    return CustomResult(result.Message, result.StatusCode);
                }
                else return CustomResult("Employee: " + MessageTexts.data_not_found, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        #endregion end Create User

        #region UserList
                
        [HttpGet]
        public async Task<IActionResult> LoadGrid(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 0,
        [FromQuery] string search = "",
        [FromQuery] string sortOrder = "")
        {
            try
            {
                var lst = await _userStrategy.LoadGrid(page, pageSize, search, sortOrder,loggedInOfficeId??0);
                return CustomResult(lst, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        #endregion UserList


        #region UserDelete
        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromBody] UserDeleteDtoRequest request)
        {
            try
            {
                var result = await _userStrategy.DeleteUserAsync(request);
                return CustomResult(result.Message, result.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        #endregion UserDelete


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetEmployeeforDropdown(int? empId)
        {
            try
            {
                return CustomResult(_employeeStrategy.GetEmployeeDropdown((loggedInOfficeId??0), empId), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

    }
}
