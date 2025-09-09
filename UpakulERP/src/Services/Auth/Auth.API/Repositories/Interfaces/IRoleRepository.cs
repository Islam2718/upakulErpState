using System.Linq.Expressions;
using Auth.API.DTO.Request;
using Auth.API.Models;
using Utility.Domain;
using Utility.Response;

namespace Auth.API.Repositories.Interfaces
{
    public interface IRoleRepository
    {

        Task<CommadResponse> CreateRoleAsync(CreateRoleDtoRequest request);
        Task<CommadResponse> UpdateRoleAsync(UpdateRoleDtoRequest request);
        Task<CommadResponse> DeleteRoleAsync(string roleId);
        Task<List<ApplicationRole>> LoadGrid(int moduleId);
        Task<ApplicationRole> GetByRoleId(int roleId);

        List<CustomSelectListItem> GetRoleByModuleIdDropdown(int moduleId);

        // Task<Roles> FindByRoleName(string roleName);

        // Task<CommadResponse> CreateRoleAsync(RoleDtoRequest request);


        //RoleXMenu GetById(int id);
        //List<RoleXMenu> GetAll();
        //Task<PaginatedRoleXMenuResponse> GetListAsync(int page, int pageSize, string search, string sortColumn, string sortDirection);
        //IEnumerable<RoleXMenu> GetMany(Expression<Func<RoleXMenu, bool>> where);
        //// insert method
        //Task<RoleXMenu> Add(RoleXMenu obj);

    }
}
