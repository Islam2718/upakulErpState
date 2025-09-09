using MediatR;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.LoanProposal
{
    public class LoanFormQuery : IRequest<LoanFormVM>
    {
        public long loanId { get; set; }
        public long loanSummaryId {  get; set; }
        public LoanFormQuery(long loanId, long loanSummaryId)
        {
            this.loanId = loanId;
            this.loanSummaryId = loanSummaryId;
        }
    }
}
