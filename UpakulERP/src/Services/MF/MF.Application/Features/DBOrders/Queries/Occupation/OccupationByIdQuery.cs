using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.Occupation
{

    public class OccupationByIdQuery : IRequest<OccupationVM>
    {
        public int id { get; set; }
        public OccupationByIdQuery(int id)
        {
            this.id = id;
        }
    }


}
