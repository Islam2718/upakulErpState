namespace Auth.API.DTO.Request
{
    public record ChangePasswordDtoRequest(string? CurrentPassword, string? NewPassword,string? ConfirmPassword);
}
