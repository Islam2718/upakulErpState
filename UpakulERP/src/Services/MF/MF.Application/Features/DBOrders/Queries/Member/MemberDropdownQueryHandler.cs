using AutoMapper;
using MediatR;
using MF.Application.Contacts.Constants;
using MF.Application.Contacts.Persistence;
using Utility.Constants;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.Member
{

    public class MemberDropdownQueryHandler : IRequestHandler<MemberDropdownQuery, List<CustomSelectListItem>>
    {
        IMemberRepository _repository;
        IMapper _mapper;

        public MemberDropdownQueryHandler(IMemberRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CustomSelectListItem>> Handle(MemberDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj = _repository.GetMany(x => (x.MemberStatus==MemberStatus.ApprovedMember) && x.OfficeId == request.officeId 
            && x.GroupId == (request.groupId ?? x.GroupId));
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "" });
            if (lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Text = s.MemberCode+" - "+s.MemberName,
                    Value = s.MemberId.ToString()
                }));
            }
            return list;
        }

    }

}
