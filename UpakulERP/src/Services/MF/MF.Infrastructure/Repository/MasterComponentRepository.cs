using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Queries.MasterComponent;
using MF.Domain.Models;
using MF.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Utility.Response;

namespace MF.Infrastructure.Repository
{
    public class MasterComponentRepository : CommonRepository<MasterComponent>, IMasterComponentRepository
    {
        AppDbContext _context;
        public MasterComponentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public MasterComponent GetById(int id)
        {
            var obj = _context.mastercomponents.FirstOrDefault(c => c.IsActive && c.Id == id);
            return obj;
        }

        public List<MasterComponent> GetAll()
        {
            var objlst = _context.mastercomponents.Where(c => c.IsActive).ToList();
            return objlst;
        }

        public IEnumerable<MasterComponent> GetMany(Expression<Func<MasterComponent, bool>> where)
        {
            var entities = _context.mastercomponents.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public async Task<PaginatedResponse<MasterComponentVM>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "Code.Contains(@0) OR Name.Contains(@0)";// OR (int)OrderNo.ToString().Contains(@0)
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "DepartmentId" : sortOrder;
            var query = _context.mastercomponents.Where(b => b.IsActive)
                 .Select(x => new MasterComponentVM
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Code = x.Code,
                 }).Where(src_qry, search).OrderBy(sortOrder).AsQueryable();

            // Pagination
            var totalRecords = await query.CountAsync();
            var dataLst = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<MasterComponentVM>(dataLst, totalRecords);
        }

        public async Task<MasterComponent> Add(MasterComponent obj)
        {
            // Add the new row to the context
            await _context.mastercomponents.AddAsync(obj);

            // Save the changes to the database
            await _context.SaveChangesAsync();
            return obj;
        }

    }
}

