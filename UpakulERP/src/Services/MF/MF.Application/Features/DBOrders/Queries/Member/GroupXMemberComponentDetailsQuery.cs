using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Domain.Models.View;
using MF.Domain.ViewModels;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.Member
{

    public class GroupXMemberComponentDetailsQuery : IRequest<List<GroupXMemberComponentDetailsVM>>
    {
        public int LogedInOfficeId { get; set; }
        public int GroupId { get; set; }
    }
}
