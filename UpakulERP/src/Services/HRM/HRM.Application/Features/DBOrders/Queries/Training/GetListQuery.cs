using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Models.Training;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.Training
{
    public class GetListQuery : IRequest<PaginatedListResponse>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
    }


}
