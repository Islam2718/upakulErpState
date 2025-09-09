using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Domain.ViewModels;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.GroupCommittee
{
    public class GetGroupXCommitteeAllDataQuery : IRequest<GroupCommitteeResponseVM>
    {
        public int groupId { get; set; }
        public GetGroupXCommitteeAllDataQuery(int? groupId)
        {
            this.groupId = groupId ?? 0;
        }
    }
}
