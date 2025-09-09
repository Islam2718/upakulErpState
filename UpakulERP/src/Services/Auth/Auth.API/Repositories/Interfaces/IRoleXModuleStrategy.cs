using Auth.API.DTO.Request;
using Auth.API.Models;
using Utility.Response;

namespace Auth.API.Repositories.Interfaces
{
    public interface IRoleXModuleStrategy
    {
        Task<CommadResponse> Create(List<RoleXModule> request);
    }
}
