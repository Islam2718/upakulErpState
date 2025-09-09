using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Accounts.Application.Contacts.Persistence;
using Accounts.Application.Features.DBOrders.Queries.BudgetEntry;
using Accounts.Domain.Models;
using Accounts.Domain.ViewModel;
using Accounts.Infrastructure.Persistence;
using EF.Core.Repository.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Accounts.Infrastructure.Repository
{
    public class BudgetEntryRepository : CommonRepository<BudgetEntry>, IBudgetEntryRepository
    {
        AppDbContext _context;

        public BudgetEntryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public BudgetEntryComponentVM GetBudgetComponents(string FinancialYear, int OfficeId, int ComponentParentId, int ComponentId)
        {
            var obj = _context.v_budgetComponentDatas.FirstOrDefault(c => c.FinancialYear == FinancialYear && c.OfficeId == OfficeId && c.ParentId == ComponentParentId && c.ComponentId == ComponentId); 
            return null;
        }

        public List<BudgetComponentData> GetAllBudgetComponents(string financialYear, int officeId, int componentParentId)  //, int componentId
        {
            var objlst = _context.v_budgetComponentDatas.Where(c => (c.FinancialYear?? financialYear) == financialYear && (c.OfficeId?? officeId) == officeId && c.ParentId == componentParentId && c.ComponentName != "Medical Expense").ToList();
            return objlst;
        }

        public List<BudgetEntry> GetAll()
        {
            var objlst = _context.budgetentry.Where(c => c.IsActive).ToList();
            return objlst;
        }

        public IEnumerable<BudgetEntry> GetMany(Expression<Func<BudgetEntry, bool>> where)
        {
            var entities = _context.budgetentry.Where(where).Where(b => b.IsActive);
            return entities;
        }


    }
}
