using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Application.Contacts.Persistence.Loan;
using MF.Application.Features.DBOrders.Queries.Member;
using MF.Domain.ViewModels;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.LoanProposal
{
    public class LoanProposalQueryHandler : IRequestHandler<LoanProposalGridQuery, PaginatedResponse<LoanApplicationVM>>
    {
        private readonly ILoanApplicationRepository _repository;
        public LoanProposalQueryHandler(ILoanApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<LoanApplicationVM>> Handle(LoanProposalGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder, request.logedInOfficeId);
        }
    }


}
