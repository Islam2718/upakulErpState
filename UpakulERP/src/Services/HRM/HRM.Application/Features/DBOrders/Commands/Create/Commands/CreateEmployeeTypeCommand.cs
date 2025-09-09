using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateEmployeeTypeCommand : IRequest<CommadResponse>
    {
      //  public int EmployeeTypeId { get; set; }
        public string EmployeeTypeName { get; set; }
       // public string EmployeeTypeValue { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }

}
