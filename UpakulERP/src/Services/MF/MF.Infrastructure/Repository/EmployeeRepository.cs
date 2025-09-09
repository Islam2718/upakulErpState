using EF.Core.Repository.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq.Expressions;
using Utility.Domain;
using Utility.Response;
using System.Linq.Dynamic.Core;
using Utility.Domain.DBDomain;
using MF.Infrastructure.Persistence;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models;

namespace MF.Infrastructure.Repository
{
    public class EmployeeRepository : CommonRepository<CommonEmployee>, IEmployeeRepository
    {
        AppDbContext _context;
        private readonly string _connectionString;
        public EmployeeRepository(AppDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
        }
        public IEnumerable<CommonEmployee> GetMany(Expression<Func<CommonEmployee, bool>> where)
        {
            var entities = _context.employees.Where(where).Where(b => b.IsActive);
            return entities;
        }

    }
}
