using Message.Domain.Models;
using Message.Infrastructure.Persistence;
using Message.Infrastructure.Repository.Interfaces;

namespace Message.Infrastructure.Repository.Strategies
{
    public class UserMailLogRepository : IUserMailLogRepository
    {
        MessageDBContext _context;
        public UserMailLogRepository(MessageDBContext  context)
        {
            _context = context;
        }

        public bool Create(UserMailLog entity)
        {
            try
            {
                _context.userMailLogs.Add(entity);
                var status = _context.SaveChanges();
                return status > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
