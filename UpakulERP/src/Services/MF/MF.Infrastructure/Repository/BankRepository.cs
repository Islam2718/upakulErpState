using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence;
using MF.Infrastructure.Persistence;
using Utility.Domain.DBDomain;

namespace MF.Infrastructure.Repository
{
    public class BankRepository : CommonRepository<CommonBank>, IBankRepository
    {
        AppDbContext _context;
        public BankRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public CommonBank GetById(int id)
        {
            var obj = _context.banks.FirstOrDefault(c => c.IsActive && c.BankId == id);
            return obj;
        }

        //public void TableActivitiesModification(CommonBank bank)
        //{
        //    try
        //    {
        //        if (_context.banks.Where(c => c.BankId == bank.BankId).Any())
        //        {
        //            var obj = GetById(bank.BankId);
        //            obj.BankName = bank.BankName;
        //            obj.BankShortCode = bank.BankShortCode;
        //            obj.BankType = bank.BankType;
        //            obj.IsActive = bank.IsActive;
        //        }
        //        else
        //            _context.banks.Add(bank);
        //        _context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
