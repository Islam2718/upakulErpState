using AutoMapper;
using MediatR;
using Project.Application.Contacts.Persistence;
using Utility.Constants;
using Utility.Domain;

namespace Project.Application.Features.DBOrders.Queries.Country
{
    public class CountryDropdownHandler : IRequestHandler<CountryDropdownQuery, List<CustomSelectListItem>>
    {
        ICountryRepository _repository;
        IMapper _mapper;

        public CountryDropdownHandler(ICountryRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CustomSelectListItem>> Handle(CountryDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj = _repository.GetAll();
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (request.id == 0 ? true : false) });
            if (lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Selected = ((s.CountryId == request.id) ? true : false),
                    Text = s.CountryName + " - " + s.CountryCode,
                    Value = s.CountryId.ToString()
                }));
            }
            return list;
        }

    }

}
