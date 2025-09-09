using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Domain;
using Utility.Enums;

namespace Global.Application.Features.DBOrders.Queries.Office
{

    public class OfficeDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public int officeId { get; set; }
        public int? officeType { get; set; }
        public OfficeDropdownQuery(int officeId, int? officeType = 0)
        {
            this.officeId = officeId;
            this.officeType = officeType ?? 0;
        }
    }


}
