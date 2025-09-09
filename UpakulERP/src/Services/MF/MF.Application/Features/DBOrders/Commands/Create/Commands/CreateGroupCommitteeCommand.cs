using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Domain.ViewModels;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateGroupCommitteeCommand : IRequest<CommadResponse>
    {
        public List<GroupCommitteeRequestVM> groupCommitteeRequests { get; set; }
        public int? loggedInEmpId { get; set; }
    }
}
