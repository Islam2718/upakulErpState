using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using EF.Core.Repository.Repository;
using Global.Application.Contacts.Persistence;
using Global.Domain.Models;
using Global.Domain.ViewModels;
using Global.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Utility.Response;

namespace Global.Infrastructure.Repository
{
    public class CountryRepository : CommonRepository<Country>, ICountryRepository
    {
        AppDbContext _context;
        public CountryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Country GetById(int id)
        {
            var obj = _context.countries.FirstOrDefault(c => c.IsActive && c.CountryId == id);
            return obj;
        }

        public List<Country> GetAll()
        {
            var objlst = _context.countries.Where(c => c.IsActive).ToList();
            return objlst;
        }

        public IEnumerable<Country> GetMany(Expression<Func<Country, bool>> where)
        {
            var entities = _context.countries.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public async Task<PaginatedResponse<CountryVM>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "CountryCode.Contains(@0) OR CountryName.Contains(@0)";
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "CountryId" : sortOrder;
            var query = _context.countries.Where(b => b.IsActive)
                .Select(x=>new CountryVM
                {
                    CountryCode = x.CountryCode,
                    CountryId = x.CountryId,
                    CountryName=x.CountryName,
                })
                .AsQueryable().Where(src_qry, search).OrderBy(sortOrder);

            // Pagination
            var totalRecords = await query.CountAsync();
            var listData = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<CountryVM>(listData, totalRecords);
        }

        public async Task<Country> Add(Country obj)
        {
            // Add the new row to the context
            await _context.countries.AddAsync(obj);

            // Save the changes to the database
            await _context.SaveChangesAsync();
            return obj;
        }

    }
}
