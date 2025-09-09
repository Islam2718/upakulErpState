using Auth.API.DTO.Request;
using Auth.API.Models;
using Utility.Response;

namespace Auth.API.Repositories.Interfaces
{
    public interface IRoleXMenuStrategy
    {
        Task<CommadResponse> Create(List<MenuPermissionRequestCommand> request,int logUserid);
    }
}
