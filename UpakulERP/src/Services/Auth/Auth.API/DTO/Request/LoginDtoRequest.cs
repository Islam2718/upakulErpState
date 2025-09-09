namespace Auth.API.DTO.Request
{
    public record LoginDtoRequest(string? UserId, string? Password/*, bool RememberMe*/);
}
