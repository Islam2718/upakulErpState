using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Queries.BoardUniversity;
using HRM.Domain.Models;
using HRM.Domain.Models.Training;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.Training
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, PaginatedListResponse>
    {
        private readonly ITrainingRepository _repository;

        public GetListQueryHandler(ITrainingRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedListResponse> Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetListAsync(
                request.Page, request.PageSize, request.Search, request.SortColumn, request.SortDirection);
        }
    }


}
