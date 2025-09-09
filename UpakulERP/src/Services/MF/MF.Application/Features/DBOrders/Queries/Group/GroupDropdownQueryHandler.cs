using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using Utility.Constants;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.Group
{
    public class GroupDropdownQueryHandler : IRequestHandler<GroupDropdownQuery, List<CustomSelectListItem>>
    {
        IGroupRepository _repository;
        IMapper _mapper;

        public GroupDropdownQueryHandler(IGroupRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CustomSelectListItem>> Handle(GroupDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj = _repository.AllGroupByOfficeId(request.officeId);
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (request.id == 0 ? true : false) });
            if (lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Selected = ((s.GroupId == request.id) ? true : false),
                    Text = s.GroupName + "-" + s.GroupCode,
                    Value = s.GroupId.ToString()
                }));
            }
            return list;
        }

    }
}
