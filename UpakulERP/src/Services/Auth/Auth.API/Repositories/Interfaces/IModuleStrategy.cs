using Auth.API.DTO;
using Auth.API.DTO.Response;
using Utility.Domain;

namespace Auth.API.Repositories.Interfaces
{
    public interface IModuleStrategy
    {
        List<RoleXModuleModel> GetModule(int userId, int? moduleId = 0);
        List<CustomSelectListItem> GetAllforDropdown();
        UserXModuleDTOResponse GetUserXModule(int employeeid);
        List<ModuleXSecurityKeyVM> GetModuleSecretKey(int? moduleId);
    }
}
