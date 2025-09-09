using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateEmployeeStatusCommand : IRequest<CommadResponse>
    {
        //public int EmployeeStatusId { get; set; }
        public string EmployeeStatusName { get; set; }
       // public char EmployeeStatusValue { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }
}
