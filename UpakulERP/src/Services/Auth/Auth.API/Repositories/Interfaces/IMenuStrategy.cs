using Auth.API.DTO;
using Auth.API.DTO.Response;
using Auth.API.Models;
using Utility.Domain;
using Utility.Response;

namespace Auth.API.Repositories.Interfaces
{
    public interface IMenuStrategy
    {
        List<ModuleXMenus> GetMenuBy(int moduleId,int roleId);
        List<MenuPermissionDTOResponse> GetMenuPermission(int moduleId, int roleId);
        List<CustomSelectListItem> GetMenubyModule(int moduleId);
        List<UserMenuVM> GetMenuListbyModule(int moduleId,int roleId);
        CommadResponse CreateMenu(UserMenu menu);
    }
}
