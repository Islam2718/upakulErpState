using Auth.API.DTO;
using Auth.API.DTO.Request;
using Auth.API.Models;
using Auth.API.Repositories.Interfaces;
using Auth.API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility.CommonController;
using Utility.Constants;
using Utility.Domain;
using static Utility.Enums.OfficeType;

namespace Auth.API.Controllers
{
    public class AccountController : ApiController
    {
        #region Variables
        private IUserStrategy _userStrategy;
        private IEmployeeStrategy _employeeStrategy;
        private ITokenService _tokenService;
        private IModuleStrategy _moduleStrategy;
        private IMenuStrategy _menuStrategy;
        private IRoleXModuleStrategy _roleXModuleStrategy;
        private IMFTransactionDateStrategy _mFTransactionDateStrategy;
        private INotificationStrategy _notificationStrategy;
        private readonly IHttpContextAccessor _hcxa;
        #endregion Variables
        public AccountController
            (IUserStrategy userStrategy, IEmployeeStrategy employeeStrategy, ITokenService tokenService, IModuleStrategy moduleStrategy
            , IMenuStrategy menuStrategy, IRoleXModuleStrategy roleXModuleStrategy, IMFTransactionDateStrategy mFTransactionDateStrategy
            , INotificationStrategy notificationStrategy
            , IHttpContextAccessor hcxa)
        {
            _userStrategy = userStrategy;
            _employeeStrategy = employeeStrategy;
            _tokenService = tokenService;
            _moduleStrategy = moduleStrategy;
            _menuStrategy = menuStrategy;
            _roleXModuleStrategy = roleXModuleStrategy;
            _mFTransactionDateStrategy = mFTransactionDateStrategy;
            _notificationStrategy = notificationStrategy;
            _hcxa = hcxa;
        }

        #region Password
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDtoRequest request)
        {
            try
            {
                var userName = User.Identity.Name;
                var result = await _userStrategy.ChangePasswordAsync(userName, request);
                return CustomResult(result.Message, result.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDtoRequest request)
        {
            try
            {
                var result = await _userStrategy.ResetPasswordAsync(request,loggedInUserInfo.userId);
                return CustomResult(result.Message, result.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        #endregion end Password

        #region    Sign IN Out
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CredentialModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login(LoginDtoRequest request)
        {
            try
            {
                var result = await _userStrategy.SignIn(request);

                if (result != null)
                {
                    if (string.IsNullOrEmpty(result.Message))
                    {
                        var emp = await _employeeStrategy.FindByEmpId(result.EmployeeId.Value);
                        if (emp != null)
                        {
                            var model = new Personal()
                            {
                                userId = result.UserName,
                                emp_code = emp.EmployeeCode,
                                employeeId = emp.EmployeeId,
                                office_type_id = emp.OfficeType,
                                office_type = (emp.OfficeType == 1 ? "Principal" : emp.OfficeType == 2 ? "Project" : emp.OfficeType == (int)OfficeTypeEnum.Zonal ? OfficeTypeEnum.Zonal.ToString() : emp.OfficeType == (int)OfficeTypeEnum.Regional ? OfficeTypeEnum.Regional.ToString() : emp.OfficeType == (int)OfficeTypeEnum.Area ? OfficeTypeEnum.Area.ToString() : emp.OfficeType == (int)OfficeTypeEnum.Branch ? OfficeTypeEnum.Branch.ToString() : ""),
                                emp_name = emp.FirstName + " " + emp.LastName??"",
                                email = result.Email,
                                office_name = emp.OfficeCode + " - " + emp.OfficeName,
                                image_url = emp.EmployeePicURL??""
                            };
                            var modules = _moduleStrategy.GetModule(userId: result.Id.Value);

                            var token = _tokenService.GenerateToken(model, userUniqueId: (result.Id ?? 0), officeTypeId: emp.OfficeType, emp.OfficeId);

                            var data = new CredentialModel()
                            {
                                token = token,
                                modules = modules,
                                personal = model
                            };
                            return CustomResult(data, HttpStatusCode.OK);
                        }
                        else return CustomResult("Employee: " + MessageTexts.data_not_found, HttpStatusCode.BadRequest);
                    }
                    else return CustomResult(result.Message, HttpStatusCode.NotFound);
                }
                else return CustomResult("Wrong userid or password", HttpStatusCode.PreconditionFailed);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //await _signInManager.SignOutAsync();
            return CustomResult("Sign out", HttpStatusCode.OK);
        }
        #endregion Sign IN Out

        #region Token Issue

        [HttpGet]
        [ProducesResponseType(typeof(CredentialModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RefreshTokenWithModuleMenu(int? moduleId, int? roleId)
        {
            try
            {
                var accessToken =  _hcxa.HttpContext.GetTokenAsync("access_token").Result;
                var isValid = _tokenService.ValidateToken(accessToken);
                if (isValid == null)
                    return CustomResult("Valid token is required", HttpStatusCode.Unauthorized);

                if (((LoggedInModuleid ?? 0) == 0 && (moduleId ?? 0) == 0) || ((LoggedInModuleRoleid ?? 0) == 0 && (roleId ?? 0) == 0))
                    return CustomResult("You are not authorized", HttpStatusCode.BadRequest);

                var modules = _moduleStrategy.GetModule(userId: loggedInUserUniqueId.Value, moduleId);
                // Transaction Date set
                string transactionDt = "";
                if (moduleId == 4 && loggedInOfficeTypeId.Value == (int)OfficeTypeEnum.Branch)
                    transactionDt = await _mFTransactionDateStrategy.GetTransactionDate(loggedInOfficeId ?? 0) ?? "";

                var menu = _menuStrategy.GetMenuListbyModule(moduleId ?? 0, roleId ?? 0);
                var token = _tokenService.GenerateRefreshToken(loggedInUserInfo, userUniqueId: loggedInUserUniqueId.Value, loggedInOfficeTypeId.Value, loggedInOfficeId.Value, moduleId.Value, roleId.Value,transactionDt);
                var notification= await _notificationStrategy.GetNotification(loggedInOfficeId??0,loggedInOfficeTypeId??0,loggedInEmployeeId??0);
                var data = new CredentialModel()
                {
                    token = token,
                    modules = modules,
                    menus = menu,
                    transactionDate = transactionDt,
                    notification= notification
                };
                return CustomResult(data, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(CredentialModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var accessToken = _hcxa.HttpContext.GetTokenAsync("access_token").Result;
                var isValid = _tokenService.ValidateToken(accessToken);
                if (isValid == null)
                    return CustomResult("Session is expired", HttpStatusCode.Unauthorized);
                if ((LoggedInModuleid ?? 0) == 0 || (LoggedInModuleRoleid ?? 0) == 0)
                    return CustomResult("You are not authorized", HttpStatusCode.BadRequest);
                var token = _tokenService.GenerateRefreshToken(loggedInUserInfo, userUniqueId: loggedInUserUniqueId.Value, officeTypeId: loggedInOfficeTypeId.Value, loggedInOfficeId.Value, LoggedInModuleid.Value, LoggedInModuleRoleid.Value, LoggedInTransactionDate ?? "");

                return CustomResult(new { token = token }, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public bool IsTokenValid([FromBody] TokenValidationRequest request)
        {
            var accessToken = _hcxa.HttpContext.GetTokenAsync("access_token").Result;
            var isValid = _tokenService.ValidateToken(accessToken);

            if (isValid == null)
                return false;
            else if (request.IsMainDashBoard)
                return true;

            else if (((LoggedInModuleid ?? 0) == 0 && (request.ModuleId ?? 0) == 0) ||
                ((LoggedInModuleRoleid ?? 0) == 0 && (request.RoleId ?? 0) == 0))
                return false;

            return true;
        }

        #endregion
    }
}
