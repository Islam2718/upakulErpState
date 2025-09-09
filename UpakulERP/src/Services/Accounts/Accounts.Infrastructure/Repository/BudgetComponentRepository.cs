using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Accounts.Application.Contacts.Persistence;
using Accounts.Domain.Models;
using Accounts.Infrastructure.Persistence;
using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Repository
{
    public class BudgetComponentRepository:CommonRepository<BudgetComponent>, IBudgetComponentRepository
    {
        AppDbContext _context;

        public BudgetComponentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public BudgetComponent GetById(int id)
        {
            var obj = _context.component.FirstOrDefault(c => c.IsActive && c.Id == id);
            return obj;
        }

        public List<BudgetComponent> GetAll()
        {
            var objlst = _context.component.Where(c => c.IsActive).ToList();
            return objlst;
        }

        public IEnumerable<BudgetComponent> GetMany(Expression<Func<BudgetComponent, bool>> where)
        {
            var entities = _context.component.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public async Task<IEnumerable<BudgetComponent>> GetComponentForDropdown(int pId)
        {
            var objlst = await _context.component.Where(c => c.IsActive && (c.ParentId ?? 0) == pId).ToListAsync();
            return objlst;
        }

        ////NoNeed
        //public async Task<IEnumerable<BudgetComponent>> GetComponentForDropdown()
        //{
        //    var objlst = await _context.component.Where(c => c.IsActive && (c.ParentId == null)).ToListAsync();
        //    return objlst;
        //}


        public async Task<List<BudgetComponent>> GetComponentListByParentId(int? parentId)
        {
            var objlst = await _context.component.Where(c => c.IsActive && (c.ParentId ?? 0) == parentId).ToListAsync();
            return objlst;
        }

        //public async Task<PaginatedBudgetComponentResponse> GetGridDataAsync(int page, int pageSize, string search, string sortColumn, string sortDirection)
        //{
        //    var query = _context.component.Where(b => b.IsActive).AsQueryable();

        //    // Searching
        //    if (!string.IsNullOrEmpty(search))
        //    {
        //        query = query.Where(u => u.ComponentName.Contains(search));
        //    }

        //    // Sorting
        //    if (typeof(BudgetComponent).GetProperty(sortColumn) != null)
        //    {
        //        query = sortDirection.ToLower() == "asc"
        //        ? query.OrderBy(x => x.ComponentName)
        //        : query.OrderByDescending(x => x.ComponentName);
        //    }


        //    // Pagination
        //    var totalRecords = await query.CountAsync();
        //    var gridData = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        //    return new PaginatedBudgetComponentResponse(gridData, totalRecords);
        //}
    }
}
