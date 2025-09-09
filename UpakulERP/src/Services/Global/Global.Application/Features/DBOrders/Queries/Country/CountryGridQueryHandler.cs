using Global.Application.Contacts.Persistence;
using Global.Domain.ViewModels;
using MediatR;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Queries.Country
{
    public class CountryGridQueryHandler : IRequestHandler<CountryGridQuery, PaginatedResponse<CountryVM>>
    {
        private readonly ICountryRepository _repository;

        public CountryGridQueryHandler(ICountryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<CountryVM>> Handle(CountryGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }
}
