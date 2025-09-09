using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence.Loan;
using MF.Domain.Models.Loan;


namespace MF.Application.Features.DBOrders.Queries.LoanProposal
{
    class LoanProposalByIdQueryHandler : IRequestHandler<LoanProposalByIdQuery, LoanApplication>
    {
        ILoanApplicationRepository _repository;
        IMapper _mapper;
        public LoanProposalByIdQueryHandler(ILoanApplicationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<LoanApplication> Handle(LoanProposalByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return obj;//_mapper.Map<LoadGridForLoanApproveVM>(obj);
        }
    }

}
