using System.Linq.Expressions;
using EF.Core.Repository.Interface.Repository;
using Utility.Domain.DBDomain;

namespace MF.Application.Contacts.Persistence
{
    public interface IEmployeeRepository : ICommonRepository<CommonEmployee>
    {
        IEnumerable<CommonEmployee> GetMany(Expression<Func<CommonEmployee, bool>> where);
    }
}
