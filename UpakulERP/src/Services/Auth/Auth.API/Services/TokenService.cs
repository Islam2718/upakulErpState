using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Utility.Constants;
using Utility.Domain;
using Utility.Enums;
using Utility.Security;

namespace Auth.API.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(Personal personal, int userUniqueId, int officeTypeId, int officeId, int? moduleId = 0, int? roleId = 0,string? tranDate="")
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(personal);
                var key = Encoding.ASCII.GetBytes(JwtSettings.SecretKey!);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, personal.userId),
                        new Claim(SessionKeys.UserUniqueId, userUniqueId.ToString()),
                        new Claim(SessionKeys.EmployeeId, personal.employeeId.ToString()),
                        new Claim(SessionKeys.OfficeTypeId, officeTypeId.ToString()),
                        new Claim(SessionKeys.OfficeId, officeId.ToString()),
                        new Claim(SessionKeys.UserGeneralInfo, json),
                        new Claim(SessionKeys.Moduleid, (moduleId??0).ToString()),
                        new Claim(SessionKeys.ModuleRoleid, (roleId??0).ToString()),
                        new Claim(SessionKeys.TransactionDate, tranDate??""),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName,personal.userId),

                    }),
                    Expires = DateTime.Now.AddMinutes(30),
                    SigningCredentials =
                        new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = JwtSettings.Issuer,
                    Audience = JwtSettings.Audience
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                
                return tokenString;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public ClaimsPrincipal? ValidateToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(JwtSettings.SecretKey!);
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = JwtSettings.Issuer,
                ValidAudience = JwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string GenerateRefreshToken(Personal personal, int userUniqueId, int officeTypeId, int officeId, int moduleId, int roleId,string tranDate)
        {
            return GenerateToken(personal, userUniqueId: userUniqueId, officeTypeId: officeTypeId, officeId: officeId, moduleId: moduleId, roleId: roleId, tranDate:tranDate);
        }
    }
}
