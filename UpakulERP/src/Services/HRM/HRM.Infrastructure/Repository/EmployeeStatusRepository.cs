using EF.Core.Repository.Repository;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpakulHRM.Infrastructure.Persistence;

namespace HRM.Infrastructure.Repository
{
    public class EmployeeStatusRepository : CommonRepository<EmployeeStatus>, IEmployeeStatusRepository
    {
        AppDbContext _context;
        public EmployeeStatusRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeStatus>> GetEmployeeStatus()
        {
            var lst = await _context.employeeStatus.Where(c => c.IsActive).ToListAsync();
            return lst;
        }
    }
}
