namespace Auth.API.DTO.Request
{
    public record UserDtoRequest( int EmployeeId, string UserName,string Email,string FirstName,string LastName, string? Password,string ConfirmPassword,int LoginUser);
    
}
