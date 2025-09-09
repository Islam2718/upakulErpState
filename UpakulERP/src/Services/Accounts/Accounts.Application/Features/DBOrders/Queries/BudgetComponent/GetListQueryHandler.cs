using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounts.Application.Contacts.Persistence;
using Accounts.Domain.Models;
using MediatR;

namespace Accounts.Application.Features.DBOrders.Queries.BudgetComponent
{
    //public class GetListQueryHandler : IRequestHandler<GetListQuery, PaginatedBudgetComponentResponse>
    //{
    //    private readonly IBudgetComponentRepository _repository;

    //    public GetListQueryHandler(IBudgetComponentRepository repository)
    //    {
    //        _repository = repository;
    //    }

    //    public async Task<PaginatedBudgetComponentResponse> Handle(GetListQuery request, CancellationToken cancellationToken)
    //    {
    //        return await _repository.GetGridDataAsync(
    //            request.Page, request.PageSize, request.Search, request.SortColumn, request.SortDirection);
    //    }
    //}
}
