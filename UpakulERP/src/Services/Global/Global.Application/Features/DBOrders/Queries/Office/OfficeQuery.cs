using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Domain;

namespace Global.Application.Features.DBOrders.Queries.GeoLocation
{
    public class OfficeQuery : IRequest<List<CustomSelectListItem>>
    {
        public int pid {  get; set; }
        public OfficeQuery(int? pid)
        {
            this.pid = pid??0;
        }
    }
}
