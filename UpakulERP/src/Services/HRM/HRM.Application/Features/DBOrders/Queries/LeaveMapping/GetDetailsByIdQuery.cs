using HRM.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Features.DBOrders.Queries.LeaveMapping
{
   public class GetDetailsByIdQuery : IRequest<OfficeTypeXConfigureDetailsVM>
    {
        public int Id { get; set; }

        public GetDetailsByIdQuery(int id)
        {
            Id = id;
        }
    }
}
