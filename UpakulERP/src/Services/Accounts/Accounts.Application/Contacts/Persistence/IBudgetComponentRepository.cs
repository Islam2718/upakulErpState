using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Accounts.Domain.Models;
using EF.Core.Repository.Interface.Repository;

namespace Accounts.Application.Contacts.Persistence
{
    public interface IBudgetComponentRepository : ICommonRepository<BudgetComponent>
    {
        BudgetComponent GetById(int id);
        List<BudgetComponent> GetAll();
        IEnumerable<BudgetComponent> GetMany(Expression<Func<BudgetComponent, bool>> where);
        Task<IEnumerable<BudgetComponent>> GetComponentForDropdown(int pid);

        Task<List<BudgetComponent>> GetComponentListByParentId(int? parentId);
        //Task<PaginatedBudgetComponentResponse> GetGridDataAsync(int page, int pageSize, string search, string sortColumn, string sortDirection);
    }

}
