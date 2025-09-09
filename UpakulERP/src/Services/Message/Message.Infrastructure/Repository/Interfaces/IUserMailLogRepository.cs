using Message.Domain.Models;

namespace Message.Infrastructure.Repository.Interfaces
{
    public interface IUserMailLogRepository
    {
        bool Create(UserMailLog entity);
    }
}
