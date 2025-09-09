using MediatR;
using Project.Application.Contacts.Persistence;
using Project.Application.Features.DBOrders.Queries.Country;
using Utility.Constants;
using Utility.Domain;

namespace Project.Application.Features.DBOrders.Queries.Doner
{
    public class DonerDropdownQueryHandler : IRequestHandler<DonerDropdownQuery, List<CustomSelectListItem>>
    {
        IDonerRepository _repository;
        public DonerDropdownQueryHandler(IDonerRepository donerRepository)
        {
            _repository = donerRepository;
        }
        public async Task<List<CustomSelectListItem>> Handle(DonerDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj = _repository.GetAll();
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "" });
            if (lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Text = s.DonerCode + " - " + s.DonerName,
                    Value = s.DonerId.ToString()
                }));
            }
            return list;
        }
    }
}
