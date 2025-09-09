using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.EmployeeRegister
{

    public class GroupByEmployeeDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public int employeeId { get; set; }
        public GroupByEmployeeDropdownQuery(int? employeeId)
        {
            this.employeeId = employeeId ?? 0;
        }
    }
}
