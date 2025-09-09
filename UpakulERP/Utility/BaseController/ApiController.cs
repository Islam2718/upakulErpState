using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility.Constants;
using System.Security.Claims;
using Utility.Domain;

namespace Utility.CommonController
{
    [ApiController]
    //[SessionExpireFilter]
    //[DisableCache]
    [ApiVersion("1")]
    [Authorize(/*AuthenticationSchemes = MVSJwtTokens.AutoSchemes*/)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class ApiController : BaseController
    {
        protected int? loggedInUserUniqueId
        {
            get
            {
                int id = 0;
                try
                {
                    if (!string.IsNullOrEmpty(this.User.FindFirstValue(SessionKeys.UserUniqueId)))
                        int.TryParse(this.User.FindFirstValue(SessionKeys.UserUniqueId), out id);
                }
                catch { }
                return id;
            }
        }

        protected Personal? loggedInUserInfo
        {
            get
            {
                Personal personal = new Personal();
                try
                {
                    if (!string.IsNullOrEmpty(this.User.FindFirstValue(SessionKeys.UserGeneralInfo)))
                        personal = Newtonsoft.Json.JsonConvert.DeserializeObject<Personal>(this.User.FindFirstValue(SessionKeys.UserGeneralInfo));
                }
                catch { }
                return personal;
            }
        }
        protected int? loggedInEmployeeId
        {
            get
            {
                int id = 0;
                try
                {
                    if (!string.IsNullOrEmpty(this.User.FindFirstValue(SessionKeys.EmployeeId)))
                        int.TryParse(this.User.FindFirstValue(SessionKeys.EmployeeId), out id);
                }
                catch { }
                return id;
            }
        }

        protected int? loggedInOfficeId
        {
            get
            {
                int id = 0;
                try
                {
                    if (!string.IsNullOrEmpty(this.User.FindFirstValue(SessionKeys.OfficeId)))
                        int.TryParse(this.User.FindFirstValue(SessionKeys.OfficeId), out id);
                }
                catch { }
                return id;
            }
        }

        protected int? loggedInOfficeTypeId
        {
            get
            {
                int id = 0;
                try
                {
                    if (!string.IsNullOrEmpty(this.User.FindFirstValue(SessionKeys.OfficeTypeId)))
                        int.TryParse(this.User.FindFirstValue(SessionKeys.OfficeTypeId), out id);
                }
                catch { }
                return id;
            }
        }

       

        protected int? LoggedInModuleid
        {
            get
            {
                int id = 0;
                try
                {
                    if (!string.IsNullOrEmpty(this.User.FindFirstValue(SessionKeys.Moduleid)))
                        int.TryParse(this.User.FindFirstValue(SessionKeys.Moduleid), out id);
                }
                catch { }
                return id;
            }
        }

        protected int? LoggedInModuleRoleid
        {
            get
            {
                int id = 0;
                try
                {
                    if (!string.IsNullOrEmpty(this.User.FindFirstValue(SessionKeys.ModuleRoleid)))
                        int.TryParse(this.User.FindFirstValue(SessionKeys.ModuleRoleid), out id);
                }
                catch { }
                return id;
            }
        }
        protected string? LoggedInTransactionDate
        {
            get
            {
                string tr_dt = "";
                try
                {
                    if (!string.IsNullOrEmpty(this.User.FindFirstValue(SessionKeys.TransactionDate)))
                        tr_dt = this.User.FindFirstValue(SessionKeys.TransactionDate);
                }
                catch { }
                return tr_dt;
            }
        }
    }
}
