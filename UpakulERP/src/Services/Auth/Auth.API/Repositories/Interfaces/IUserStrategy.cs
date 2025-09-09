using Auth.API.DTO.Request;
using Auth.API.DTO.Response;
using Auth.API.Models;
using Utility.Response;

namespace Auth.API.Repositories.Interfaces
{
    public interface IUserStrategy
    {
        Task<CommadResponse> CreateUserAsync(UserDtoRequest request);
        Task<CommadResponse> ChangePasswordAsync(string UserName, ChangePasswordDtoRequest request);
        Task<CommadResponse> ResetPasswordAsync(ResetPasswordDtoRequest request,string user);
        Task<LoginDtoResponse> SignIn(LoginDtoRequest request);
        Task<ApplicationUser> GetById(int id);
        Task<PaginatedResponse<UsersGridResponse>> LoadGrid(int page, int pageSize, string search, string sortOrder,int officeId);
        Task<CommadResponse> DeleteUserAsync(UserDeleteDtoRequest request);
    }
}
