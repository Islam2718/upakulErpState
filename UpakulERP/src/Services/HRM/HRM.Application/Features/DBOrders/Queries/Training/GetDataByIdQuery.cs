using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Application.Features.DBOrders.Queries.BoardUniversity;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.Training
{
    public class GetDataByIdQuery : IRequest<TrainingVM>
    {
        public int id { get; set; }
        public GetDataByIdQuery(int id)
        {
            this.id = id;
        }
    }

}
