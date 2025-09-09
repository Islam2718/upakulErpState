using MF.Infrastructure.Persistence;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence;
using System.Linq.Expressions;
using MF.Domain.Models.Loan;

namespace MF.Infrastructure.Repository
{
    public class MRAPurposeRepository : CommonRepository<MRAPurpose>, IMRAPurposeRepository
    {
        AppDbContext _context;
        public MRAPurposeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        //public List<MRAPurpose> GetAll()
        //{
        //    var objlst = _context.mRAPurposes.Where(c => c.IsActive).ToList();
        //    return objlst;
        //}
        public IEnumerable<MRAPurpose> GetMany(Expression<Func<MRAPurpose, bool>> where)
        {
            var entities = _context.mRAPurposes.Where(where).Where(b => b.IsActive);
            return entities;
        }
    }
}
