using MediatR;

namespace MF.Application.Features.DBOrders.Queries.LoanApproval
{
    public class GetAllQuery : IRequest<List<Domain.Models.Loan.LoanApproval>>
    {
        public GetAllQuery()
        { }
    }
}
