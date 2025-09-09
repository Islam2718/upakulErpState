using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using HRM.Domain.Models;
using Utility.Domain;
using HRM.Domain.ViewModels;

namespace HRM.Application.Features.DBOrders.Queries.LeaveMapping
{
    public class GetMasterAllQuery : IRequest<List<OfficeTypeXConfigMasterVM>>
    {
        public GetMasterAllQuery()
        { }
    }
}
