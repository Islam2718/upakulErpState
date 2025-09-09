using MediatR;
using MF.Domain.Models.Loan;

namespace MF.Application.Features.DBOrders.Queries.LoanProposal
{
    public class LoanProposalByIdQuery : IRequest<LoanApplication>
    {
        public int id { get; set; }
        public LoanProposalByIdQuery(int id)
        {
            this.id = id;
        }
    }


}
