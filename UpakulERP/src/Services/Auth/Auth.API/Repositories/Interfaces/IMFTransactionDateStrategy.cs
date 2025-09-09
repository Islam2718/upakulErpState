namespace Auth.API.Repositories.Interfaces
{
    public interface IMFTransactionDateStrategy
    {
        Task<string?> GetTransactionDate(int officeId);
    }
}
