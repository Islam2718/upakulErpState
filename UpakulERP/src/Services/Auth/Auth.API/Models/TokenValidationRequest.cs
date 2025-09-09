namespace Auth.API.Models
{
    public class TokenValidationRequest
    {
        public string? Token { get; set; }
        public int? ModuleId { get; set; }
        public int? RoleId { get; set; }
        public bool IsMainDashBoard { get; set; } = false; 
    }
}
