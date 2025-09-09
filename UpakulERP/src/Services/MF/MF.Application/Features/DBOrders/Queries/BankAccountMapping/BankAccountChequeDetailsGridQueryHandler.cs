using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.ViewModels;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.BankAccountMapping
{
    public class BankAccountChequeDetailsGridQueryHandler : IRequestHandler<BankAccountChequeDetailsGridQuery, PaginatedResponse<BankAccountChequeDetailsVM>>
    {
        private readonly IBankAccountMappingRepository _repository;

        public BankAccountChequeDetailsGridQueryHandler(IBankAccountMappingRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<BankAccountChequeDetailsVM>> Handle(BankAccountChequeDetailsGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadChequeDetailsGrid(request.Page, request.PageSize, request.Search, request.SortOrder, request.BankAccountMappingId);
        }
    }
}
