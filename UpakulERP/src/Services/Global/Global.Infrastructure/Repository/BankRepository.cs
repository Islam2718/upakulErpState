using EF.Core.Repository.Repository;
using Global.Application.Contacts.Persistence;
using Global.Domain.Models;
using Global.Domain.ViewModels;
using Global.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Utility.Response;

namespace Global.Infrastructure.Repository
{
    class BankRepository : CommonRepository<Bank>, IBankRepository
    {
        AppDbContext _context;
        public BankRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
       
        public Bank GetById(int id)
        {
            var obj = _context.banks.FirstOrDefault(c => c.IsActive && c.BankId == id);
            return obj;
        }

        
        public async Task<PaginatedResponse<BankVM>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search ?? "0";
            string src_Qry = string.IsNullOrEmpty(search) ? "@0=@0" : "BankName.Contains(@0) OR BankShortCode.Contains(@0) OR BankTypeFull.Contains(@0)";
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "BankId" : sortOrder;
            var query = _context.banks.Where(b => b.IsActive == true)
                .Select(s => new BankVM
                 {
                     BankId = s.BankId,
                     BankType = s.BankType,
                     BankTypeFull = _context.GetBankType(s.BankType),
                     BankName = s.BankName,
                     BankShortCode = s.BankShortCode,
                 }).Where(src_Qry, search).OrderBy(sortOrder).AsQueryable();

            

            // Pagination
            var totalRecords = await query.CountAsync();
            var banks = await query.Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            return new PaginatedResponse<BankVM>(banks, totalRecords);
        }
        public IEnumerable<Bank> GetMany(Expression<Func<Bank, bool>> where)
        {
            var entities = _context.banks.Where(where).Where(b => b.IsActive);
            return entities;
        }
    }
}
