using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contacts.Persistence;
using Project.Domain.Models;
using Project.Infrastructure.Persistence;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Utility.Response;
using roject.Domain.ViewModels;

namespace Project.Infrastructure.Repository
{
    public class DonerRepository : CommonRepository<Doner>, IDonerRepository
    {
        AppDbContext _context;
        public DonerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Doner GetById(int id)
        {
            var obj = _context.doners.FirstOrDefault(c => c.IsActive && c.DonerId == id);
            return obj;
        }

        public IEnumerable<Doner> GetMany(Expression<Func<Doner, bool>> where)
        {
            var entities = _context.doners.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public async Task<PaginatedResponse<DonerVM>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search ?? "0";
            string src_Qry = string.IsNullOrEmpty(search) ? "@0=@0" : "DonerCode.Contains(@0) OR DonerName.Contains(@0) OR CountryName.Contains(@0)";
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "DonerId" : sortOrder;
            var query = (from d in _context.doners
                         join c in _context.countries on d.CountryId equals c.CountryId
                         where d.IsActive && c.IsActive
                         select new DonerVM
                         {
                             CountryId = d.CountryId,
                             CountryName = c.CountryName,
                             DonerCode = d.DonerCode,
                             DonerId = d.DonerId,
                             DonerName = d.DonerName,
                             FirstContactPersonContactNo = d.FirstContactPersonContactNo,
                             FirstContactPersonName = d.FirstContactPersonName,
                             Location = d.Location,
                             SecendContactPersonContactNo = d.SecendContactPersonContactNo,
                             SecendContactPersonName = d.SecendContactPersonName
                         }
                ).Where(src_Qry, search).OrderBy(sortOrder).AsQueryable();

            // Pagination
            var totalRecords = await query.CountAsync();
            var lst = await query.Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            return new PaginatedResponse<DonerVM>(lst, totalRecords);
        }
    }
}
