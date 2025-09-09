using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence.Loan;

namespace MF.Application.Features.DBOrders.Queries.LoanApproval
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<Domain.Models.Loan.LoanApproval>>
    {
        ILoanApprovalRepository _repository;
        IMapper _mapper;

        public GetAllQueryHandler(ILoanApprovalRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Domain.Models.Loan.LoanApproval>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetAll();
        }

    }
}
