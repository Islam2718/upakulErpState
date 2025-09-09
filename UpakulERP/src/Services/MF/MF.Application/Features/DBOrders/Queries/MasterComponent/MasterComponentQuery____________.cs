using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.MasterComponent
{
    public class MasterComponentQuery____________ : IRequest<List<CustomSelectListItem>>
    {
        public int id { get; set; }
        public MasterComponentQuery____________(int? id)
        {
            this.id = id ?? 0;
        }
    }
}
