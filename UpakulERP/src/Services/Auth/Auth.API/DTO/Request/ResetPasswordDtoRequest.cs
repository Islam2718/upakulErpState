namespace Auth.API.DTO.Request
{
    public record ResetPasswordDtoRequest(string? UserName, string? NewPassword= "1abc#CSF");
}
