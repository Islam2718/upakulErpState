using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.Office
{
    public class OfficeDropdownQueryHandler : IRequestHandler<OfficeDropdownQuery, List<CustomSelectListItem>>
    {
        IOfficeRepository _repository;
        IMapper _mapper;

        public OfficeDropdownQueryHandler(IOfficeRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CustomSelectListItem>> Handle(OfficeDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj = _repository.GetOfficeDropdown(request.officeId, request.officeType.Value);
            var list = new List<CustomSelectListItem>();
            //list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (request.officeId == 0 ? true : false) });
            if (lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Selected = ((s.OfficeId == request.officeId) ? true : false),
                    Text = s.OfficeCode + " - " + s.OfficeName,
                    Value = s.OfficeId.ToString()
                }));
            }
            return list;
        }
    }
}
