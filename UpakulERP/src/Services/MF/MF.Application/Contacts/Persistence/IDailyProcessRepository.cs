using System.Linq.Expressions;
using EF.Core.Repository.Interface.Repository;
using MF.Domain.Models;

namespace MF.Application.Contacts.Persistence
{
    public interface IDailyProcessRepository : ICommonRepository<DailyProcess>
    {
        DailyProcess GetById(int id);
        //List<DailyProcess> GetAll();
        IEnumerable<DailyProcess> GetMany(Expression<Func<DailyProcess, bool>> where);        
        IEnumerable<HolyDay> CheckHolidays(Expression<Func<HolyDay, bool>> where);
    }
}
