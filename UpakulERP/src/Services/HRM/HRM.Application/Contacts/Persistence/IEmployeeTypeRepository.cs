using EF.Core.Repository.Interface.Repository;
using HRM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Contacts.Persistence
{
    public interface IEmployeeTypeRepository : ICommonRepository<EmployeeType>
    {
        Task<IEnumerable<EmployeeType>> GetEmployeeType();
    }
}
