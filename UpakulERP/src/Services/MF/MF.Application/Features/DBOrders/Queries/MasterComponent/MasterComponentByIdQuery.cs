using MediatR;
using MF.Application.Features.DBOrders.Queries.MasterComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Application.Features.DBOrders.Queries.MasterComponent
{
    public class MasterComponentByIdQuery : IRequest<MasterComponentVM>
    {
        public int id { get; set; }
        public MasterComponentByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
