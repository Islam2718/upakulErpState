using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contacts.Persistence;
using Project.Domain.Models;
using Project.Domain.ViewModels;
using Project.Infrastructure.Persistence;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Utility.Response;

namespace Project.Infrastructure.Repository
{

    public class ProjectRepository : CommonRepository<Projects>, IProjectRepository
    {
        AppDbContext _context;
        public ProjectRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Projects GetById(int id)
        {
            var obj = _context.projects.FirstOrDefault(c => c.IsActive && c.ProjectId == id);
            return obj;
        }
        
        public IEnumerable<Projects> GetMany(Expression<Func<Projects, bool>> where)
        {
            var entities = _context.projects.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public async Task<PaginatedResponse<ProjectVM>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search ?? "0";
            string src_Qry = string.IsNullOrEmpty(search) ? "@0=@0" : "BankName.Contains(@0) OR BankShortCode.Contains(@0) OR BankTypeFull.Contains(@0)";
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "BankId" : sortOrder;
            var query = (from p in _context.projects
                        join d in _context.doners on p.DonerId equals d.DonerId
                        join e in _context.employees on p.ChipEmployeeId equals e.EmployeeId
                        where p.IsActive 
                        select new ProjectVM
                        {
                           ProjectId = p.ProjectId,
                           ExpireDate = p.ProjectEndDate,
                           ProjectTitle = p.ProjectTitle,
                           ProjectShortName = p.ProjectShortName,
                           ChipEmployee=e.EmployeeCode+" - "+e.EmployeeFullName,
                           DonerName=d.DonerName,
                        }).Where(src_Qry, search).OrderBy(sortOrder).AsQueryable();
                

            // Pagination
            var totalRecords = await query.CountAsync();
            var lst = await query.Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            return new PaginatedResponse<ProjectVM>(lst, totalRecords);
        }
    }
}
