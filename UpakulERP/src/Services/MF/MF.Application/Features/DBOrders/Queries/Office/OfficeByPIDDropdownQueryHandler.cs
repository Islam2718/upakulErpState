using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using Utility.Constants;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.Office
{
    public class OfficeByPIDDropdownQueryHandler : IRequestHandler<OfficeByPIDDropdownQuery, List<CustomSelectListItem>>
    {
        IOfficeRepository _repository;
        IMapper _mapper;
        public OfficeByPIDDropdownQueryHandler(IOfficeRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(OfficeByPIDDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj = await _repository.GetOfficeByParentId(request.pid);
            var list = new List<CustomSelectListItem>();
            //list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (request.pid == 0 ? true : false) });
            if (lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Selected = ((s.OfficeId == request.pid) ? true : false),
                    Text = s.OfficeCode + " - " + s.OfficeName,
                    Value = s.OfficeId.ToString()
                }));
            }
            return list;
        }
    }
}
