using System.Security.Claims;
using Auth.API.DTO;
using Utility.Domain;

namespace Auth.API.Services
{
    public interface ITokenService
    {
        string GenerateToken(Personal personal, int userUniqueId, int officeTypeId, int officeId, int? moduleId = 0, int? roleId = 0, string? tranDate = "");
        ClaimsPrincipal? ValidateToken(string token);
        string GenerateRefreshToken(Personal personal, int userUniqueId, int officeTypeId, int officeId, int moduleId, int roleId, string tranDate);
    }
}
