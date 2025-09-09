namespace Auth.API.DTO.Response
{
    public record LoginDtoResponse(int? Id=0,int? EmployeeId = 0, string? UserName="", string? Email = "", string? FirstName = "", string? LastName = "",string? Message="") { }
}
