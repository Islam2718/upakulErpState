using Global.Application.Contacts.Persistence;
using Global.Domain.Models;
using Global.Domain.Models.Views;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Queries.Office
{
    public class OfficesGridQueryHandler : IRequestHandler<OfficesGridQuery, PaginatedResponse<VWOffice>>
    {
        private readonly IOfficeRepository _repository;

        public OfficesGridQueryHandler(IOfficeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<VWOffice>> Handle(OfficesGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }
}
