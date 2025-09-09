using System.Data;
using Auth.API.Context;
using Auth.API.DTO;
using Auth.API.DTO.Response;
using Auth.API.Repositories.Interfaces;
using Utility.Constants;
using Utility.Domain;

namespace Auth.API.Repositories.Strategies
{
    public class ModuleStrategy(AppDbContext context) : IModuleStrategy
    {
        public List<RoleXModuleModel> GetModule(int userId, int? moduleId = 0)
        {
            var lst = (from mod in context.modules
                       join rm in context.roleXModules on mod.ModuleId equals rm.ModuleId
                       where mod.IsActive && rm.IsActive && rm.UserId == userId
                       && mod.ModuleId != moduleId
                       select new RoleXModuleModel()
                       {
                           module_id = mod.ModuleId,
                           role_id = rm.RoleId,
                           display_order = mod.DisplayOrder,
                           icon_class = mod.ModuleIconClass,
                           module_name = mod.ModuleName,
                           secend_div_class = mod.ModuleSecendDivClass,
                           title = mod.ModuleTitle,
                           url = mod.ModuleURL

                       }).OrderBy(x => x.display_order).ToList();
            return lst;
        }

        public List<CustomSelectListItem> GetAllforDropdown()
        {
            var lst = new List<CustomSelectListItem>();
            lst.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "" });

            lst.AddRange(context.modules.Where(x => x.IsActive).Select(x =>
            new CustomSelectListItem
            {
                Text = x.ModuleTitle + " (" + x.ModuleName + ")",
                Value = x.ModuleId.ToString()
            }).OrderBy(x => x.Text).ToList());
            return lst;
        }

        public UserXModuleDTOResponse GetUserXModule(int employeeid)
        {
            UserXModuleDTOResponse response = new UserXModuleDTOResponse();
            List<UserXModule> lst = new List<UserXModule>();
            try
            {
                var emps = context.employees.First(x => x.EmployeeId == employeeid);

                response.FirstName = emps.FirstName;
                response.LastName = emps.LastName;
                response.employeeId = employeeid;
                var user = context.Users.FirstOrDefault(x => x.EmployeeId == emps.EmployeeId);
                int userId = 0;
                if (user != null)
                {
                    response.userName = user.UserName;
                    userId = response.userId = user.Id;
                }

                var modules = context.modules.Where(x => x.IsActive).ToList();
                var lst_roles = context.Roles.ToList();
                var roleModule = context.roleXModules.Where(x => x.IsActive && x.UserId == userId).ToList();
                foreach (var item in modules)
                {
                    if (lst_roles.Where(x => x.ModuleId == item.ModuleId).Any())
                    {
                        UserXModule obj = new UserXModule()
                        {
                            moduleId = item.ModuleId,
                            moduleName = item.ModuleTitle + " (" + item.ModuleName + ")"
                        };
                        int roleId = 0;
                        if (roleModule.Where(x => x.ModuleId == item.ModuleId).Any())
                        {
                            roleId = roleModule.First(x => x.ModuleId == item.ModuleId).RoleId;
                            obj.isSelected = true;
                        }

                        var listItem = new List<CustomSelectListItem>();
                        //listItem.Add(new SelectListItem { Selected = (roleId == 0 ? true : false), Text = "Select Role" });
                        listItem.AddRange(lst_roles.Where(x => x.ModuleId == item.ModuleId)
                            .Select(x => new CustomSelectListItem
                            {
                                Selected = (x.Id == roleId ? true : false),
                                Text = x.Name,
                                Value = x.Id.ToString()
                            }));
                        obj.roles = listItem;
                        lst.Add(obj);
                    }

                }
                response.userXModule = lst;

            }
            catch (Exception ex)
            {
                response = new UserXModuleDTOResponse();
            }
            return response;
        }

        public List<ModuleXSecurityKeyVM> GetModuleSecretKey(int? moduleId)
        {
            try
            {
               return context.modules.Where(x => x.IsActive && x.ModuleId == (moduleId ?? x.ModuleId))
               .Select(s => new ModuleXSecurityKeyVM
               {
                   ModuleId = s.ModuleId,
                   SecurityKey = s.SecurityKey,
               }).ToList();
            }
            catch 
            {
                return new List<ModuleXSecurityKeyVM>();
            }
        }
    }
}
