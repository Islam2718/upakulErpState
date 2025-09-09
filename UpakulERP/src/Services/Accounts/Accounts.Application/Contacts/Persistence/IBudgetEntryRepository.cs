using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Accounts.Domain.Models;
using Accounts.Domain.ViewModel;
using EF.Core.Repository.Interface.Repository;

namespace Accounts.Application.Contacts.Persistence
{
    public interface IBudgetEntryRepository : ICommonRepository<BudgetEntry>
    {
        //BudgetEntryComponentVM GetBudgetComponents(string FinancialYear, int OfficeId, int ComponentParentId, int ComponentId);
        List<BudgetEntry> GetAll();
        //, int ComponentId
        List<BudgetComponentData> GetAllBudgetComponents(string FinancialYear, int OfficeId, int ComponentParentId);
        IEnumerable<BudgetEntry> GetMany(Expression<Func<BudgetEntry, bool>> where);

    }
}
