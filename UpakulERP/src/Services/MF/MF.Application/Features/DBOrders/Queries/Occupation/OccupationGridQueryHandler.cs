using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.Occupation
{
    public class OccupationGridQueryHandler : IRequestHandler<OccupationGridQuery, PaginatedResponse<OccupationVM>>
    {
        private readonly IOccupationRepository _repository;

        public OccupationGridQueryHandler(IOccupationRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<OccupationVM>> Handle(OccupationGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }
}
