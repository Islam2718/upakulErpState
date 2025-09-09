namespace Auth.API.DTO.Request
{
    public record UserDtoResponse(int RoleId, int EmployeeId, string UserName, string Email, string FirstName, string LastName, string? Password, string? ConfirmPassword) { }
    
}
