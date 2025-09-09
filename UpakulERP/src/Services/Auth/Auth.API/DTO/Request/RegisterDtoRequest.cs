namespace Auth.API.DTO.Request
{
    //public record RegisterDtoRequest(int RoleId, int EmployeeId, string UserName, string? Password,string ConfirmPassword);
    public class RegisterDtoRequest
    {
        public int? UserId { get; set; }
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public List<RolesXModuleViewmodel> rolesXModules { get; set; }
    }
    public class RolesXModuleViewmodel
    {
        public int ModuleId { get; set; }
        public int RoleId { get; set; }
    }
}
