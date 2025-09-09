using MF.Application.Contacts.Persistence.Loan;
using MF.Domain.Models.Functions;
using MF.Domain.Models.Loan;
using MF.Infrastructure.Persistence;

namespace MF.Infrastructure.Repository.Loan
{
    public class LoanSummaryRepository : ILoanSummaryRepository
    {
        AppDbContext _context;
        public LoanSummaryRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool NewDisbursed(LoanSummary model, int durationInMonth, int noOfSchedule)
        {
            DateTime scheduleDate = model.DisburseDate.AddDays(model.GracePeriod);
            try
            {
                var lst = _context.RepaymentSchedule(model.OfficeId, model.GroupId, model.LoanApplicationId, model.PrincipleAmount, model.InterestRate, durationInMonth, noOfSchedule, scheduleDate, model.PaymentFrequency).ToList();
                if (lst.Any())
                {
                    model.ServiceCharge = (decimal)lst.Sum(x => (x.ServiceCharge ?? 0));
                    _context.loanSummaries.Add(model);
                    _context.SaveChanges();
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
