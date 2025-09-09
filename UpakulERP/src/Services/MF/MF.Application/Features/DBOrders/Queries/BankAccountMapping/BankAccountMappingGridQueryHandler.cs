using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Queries.Member;
using MF.Domain.Models;
using MF.Domain.Models.View;
using MF.Domain.ViewModels;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.BankAccountMapping
{
    public class BankAccountMappingGridQueryHandler : IRequestHandler<BankAccountMappingGridQuery, PaginatedResponse<BankAccountMappingVM>>
    {
        private readonly IBankAccountMappingRepository _repository;

        public BankAccountMappingGridQueryHandler(IBankAccountMappingRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<BankAccountMappingVM>> Handle(BankAccountMappingGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder, request.logedInOfficeId);
        }
    }

}
