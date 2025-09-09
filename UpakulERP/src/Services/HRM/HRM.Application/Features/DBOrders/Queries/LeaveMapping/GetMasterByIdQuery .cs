using HRM.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Features.DBOrders.Queries.LeaveMapping
{
    public class GetMasterByIdQuery : IRequest<OfficeTypeXConfigMasterVM>
    {
        public int Id { get; set; }

        public GetMasterByIdQuery(int id)
        {
            Id = id;
        }
    }
}
