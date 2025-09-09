using MF.Domain.Models.Loan;

namespace MF.Application.Contacts.Persistence.Loan
{
    public interface ILoanSummaryRepository
    {
        bool NewDisbursed(LoanSummary model, int durationInMonth, int noOfSchedule);
    }
}
