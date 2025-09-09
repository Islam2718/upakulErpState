using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Domain.ViewModels;
using MediatR;

namespace Global.Application.Features.DBOrders.Queries.Country
{
    public class CountryByIdHandler : IRequestHandler<CountryByIdQuery, CountryVM>
    {
        ICountryRepository _repository;
        IMapper _mapper;
        public CountryByIdHandler(ICountryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CountryVM> Handle(CountryByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return _mapper.Map<CountryVM>(obj);
        }
    }
}
