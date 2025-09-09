using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.Group
{ 
    public class EmployeeXGroupDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public int empId { get; set; }
        public EmployeeXGroupDropdownQuery(int? empId)
        {
            this.empId = empId ?? 0;
        }
    }

}
