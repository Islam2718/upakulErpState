using System.Linq.Expressions;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence.Loan;
using MF.Domain.Models.Loan;
using MF.Infrastructure.Persistence;

namespace MF.Infrastructure.Repository.Loan
{

    public class LoanApprovalRepository : CommonRepository<LoanApproval>, ILoanApprovalRepository
    {
        AppDbContext _context;
        public LoanApprovalRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public LoanApproval GetById(int level)
        {
            var obj = _context.loanApprovals.FirstOrDefault(c => c.Level ==level);
            return obj;
        }
        public bool IsValidEmployee(int employeeId,int currentLevel,string approvalType,int poposedAmt)
        {
            return (from emp in _context.employees
                    join lap in _context.loanApprovals on emp.DesignationId equals lap.DesignationId
                    where emp.EmployeeId == employeeId && lap.Level>=currentLevel && lap.ApprovalType==approvalType
                    && lap.StartingValueAmount<=poposedAmt
                    select lap.Level).Any();
        }
        public List<LoanApproval> GetAll()
        {
            return _context.loanApprovals.OrderBy(x => x.Level).ToList();
        }
        public IEnumerable<LoanApproval> GetMany(Expression<Func<LoanApproval, bool>> where)
        {
            var entities = _context.loanApprovals.Where(where);
            return entities;
        }

        public bool Delete(int level)
        {
            var obj = GetById(level);
             _context.loanApprovals.Remove(obj);
             _context.SaveChanges();
            var lst = GetMany(x => x.Level > level);
            foreach (var item in lst)
            {
                _context.loanApprovals.Remove(item);
                _context.SaveChanges();
                item.Level = level;
                _context.loanApprovals.Add(item);
                _context.SaveChanges();
                level += 1;
            }
            return true;
        }
    }
}

