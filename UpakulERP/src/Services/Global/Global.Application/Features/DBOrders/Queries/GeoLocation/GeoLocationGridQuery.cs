using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Global.Domain.Models;
using Global.Domain.Models.Views;
using Global.Domain.ViewModels;
using MediatR;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Queries.GeoLocation
{
    public class GeoLocationGridQuery : IRequest<PaginatedResponse<VWGeoLocation>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
    }
}
