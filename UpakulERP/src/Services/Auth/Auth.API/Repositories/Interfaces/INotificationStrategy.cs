using Utility.Domain.DBDomain;

namespace Auth.API.Repositories.Interfaces
{
    public interface INotificationStrategy
    {
        Task<Notification> GetNotification(int officeId, int officeTypeId, int loggedinEmpId);
    }
}
