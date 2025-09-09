using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models.View;
using MF.Domain.ViewModels;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.Member
{
    public class GroupXMemberComponentDetailsQueryHandler : IRequestHandler<GroupXMemberComponentDetailsQuery, List<GroupXMemberComponentDetailsVM>>
    {
        private readonly IMemberRepository _repository;

        public GroupXMemberComponentDetailsQueryHandler(IMemberRepository repository)
        {
            _repository = repository;
        }

        public Task<List<GroupXMemberComponentDetailsVM>> Handle(GroupXMemberComponentDetailsQuery request, CancellationToken cancellationToken)
        {
            return _repository.GroupXMemberComponentDetails(request.LogedInOfficeId, request.GroupId);
        }
    }


}
