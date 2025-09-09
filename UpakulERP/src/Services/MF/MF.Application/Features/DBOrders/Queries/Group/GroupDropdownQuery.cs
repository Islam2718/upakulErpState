using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.Group
{
    public class GroupDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public int id { get; set; }
        public int officeId { get; set; }
        public GroupDropdownQuery(int? id, int? officeId)
        {
            this.id = id ?? 0;
            this.officeId = officeId ?? 0; 
        }
    }

}
