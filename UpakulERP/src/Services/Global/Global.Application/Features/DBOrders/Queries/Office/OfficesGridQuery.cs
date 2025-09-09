using Global.Domain.Models;
using Global.Domain.Models.Views;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Queries.Office
{
    public class OfficesGridQuery : IRequest<PaginatedResponse<VWOffice>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
    }
}
