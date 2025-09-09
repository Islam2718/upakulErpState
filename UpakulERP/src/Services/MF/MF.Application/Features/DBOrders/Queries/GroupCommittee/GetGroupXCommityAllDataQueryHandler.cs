using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Enums;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Queries.Group;
using MF.Domain.ViewModels;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.GroupCommittee
{
    public class GetGroupXCommityAllDataQueryHandler : IRequestHandler<GetGroupXCommitteeAllDataQuery, GroupCommitteeResponseVM>  //List<CustomSelectListItem>
    {
        IMemberRepository _repository;
        IGroupCommitteeRepository _repositoryGroupCommittee;
        IMapper _mapper;

        public GetGroupXCommityAllDataQueryHandler(IMemberRepository repository, IMapper mapper, IGroupCommitteeRepository repositoryGroupCommittee)
        {
            _repository = repository;
            _mapper = mapper;
            _repositoryGroupCommittee = repositoryGroupCommittee;
        }

        public async Task<GroupCommitteeResponseVM> Handle(GetGroupXCommitteeAllDataQuery request, CancellationToken cancellationToken)
        {
            var obj = new GroupCommitteeResponseVM();
            obj.members = _repository.GetMemberByGroupIdDropdown(request.groupId);
            obj.committee = new GroupXMemberPosition().GetGroupXMemberPositionDropDown();
            obj.groupCommitteeDatas = _repositoryGroupCommittee.GetActiveCommitteeMember(request.groupId);
            return obj;
        }
    }
}
