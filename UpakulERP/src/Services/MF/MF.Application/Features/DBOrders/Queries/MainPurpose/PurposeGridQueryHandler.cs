using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Queries.MainPurpose;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.Office
{
    public class PurposeGridQueryHandler : IRequestHandler<PurposeGridQuery, PaginatedResponse<PurposeForGridVM>>
    {
        private readonly IPurposeRepository _purposeRepository;

        public PurposeGridQueryHandler(IPurposeRepository purposeRepository)
        {
            _purposeRepository = purposeRepository;
        }

        public async Task<PaginatedResponse<PurposeForGridVM>> Handle(PurposeGridQuery request, CancellationToken cancellationToken)
        {
            var lst = await _purposeRepository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder);
            if (lst.Any())
                return new PaginatedResponse<PurposeForGridVM>(lst, lst.First().TotalCount);
            else return new PaginatedResponse<PurposeForGridVM>(new List<PurposeForGridVM>(), 0);
        }
    }
}
