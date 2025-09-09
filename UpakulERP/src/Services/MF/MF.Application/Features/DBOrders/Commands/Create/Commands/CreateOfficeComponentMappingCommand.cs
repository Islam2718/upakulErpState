using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateOfficeComponentMappingCommand : IRequest<CommadResponse>
    {
        public int ComponentId { get; set; }
        public List<int> SelectedBranch { get; set; }
        public int? loggedInEmployeeId { get; set; }
        //public DateTime? changeDate { get; set; } = DateTime.Now;
    }


}
