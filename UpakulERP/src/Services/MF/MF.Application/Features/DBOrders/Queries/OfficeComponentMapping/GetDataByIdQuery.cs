using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.OfficeComponentMapping
{
    public class GetDataByIdQuery : IRequest<List<OfficeComponentMappingVM>>
    {
        public int id { get; set; }
        public GetDataByIdQuery(int id)
        {
            this.id = id;
        }
    }

}
