using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace HRM.Application.Features.DBOrders.Queries.Education
{
    public class EducationQuery : IRequest<List<CustomSelectListItem>>
    {
        public int pid { get; set; }
        public EducationQuery(int? pid)
        {
            this.pid = pid ?? 0;
        }
    }
}
