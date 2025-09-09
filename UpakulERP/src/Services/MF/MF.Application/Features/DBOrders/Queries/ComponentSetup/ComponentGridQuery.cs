using MediatR;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using System;
using System.Collections.Generic;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.ComponentSetup
{

    public class ComponentGridQuery : IRequest<PaginatedResponse<ComponentVM>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
    }
}
