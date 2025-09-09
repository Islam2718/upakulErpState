using MF.Infrastructure.Persistence;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence;
using System.Linq.Expressions;
using MF.Domain.Models.Loan;

namespace MF.Infrastructure.Repository
{
    public class MainPurposeRepository : CommonRepository<Purpose>, IMainPurposeRepository
    {
        AppDbContext _context;
        public MainPurposeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Purpose> GetMany(Expression<Func<Purpose, bool>> where)
        {
            var entities = _context.mainPurposes.Where(where).Where(b => b.IsActive);
            return entities;
        }
    }
}
