using EF.Core.Repository.Interface.Repository;
using HRM.Domain.Models;

namespace HRM.Application.Contacts.Persistence
{
    public interface IEmployeeStatusRepository : ICommonRepository<EmployeeStatus>
    {
        Task<IEnumerable<EmployeeStatus>> GetEmployeeStatus();        
    }
}
