using MediatR;
using MF.Application.Contacts.Persistence.Loan;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.LoanProposal
{
    public class LoanFormQueryHandler : IRequestHandler<LoanFormQuery, LoanFormVM>
    {
        ILoanApplicationRepository _repository;
        public LoanFormQueryHandler(ILoanApplicationRepository repository)
        {
            _repository = repository;
        }
        public async Task<LoanFormVM> Handle(LoanFormQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetLoanForm(request.loanId, loanSummaryId: request.loanSummaryId);
            return obj;
        }
    }
}
