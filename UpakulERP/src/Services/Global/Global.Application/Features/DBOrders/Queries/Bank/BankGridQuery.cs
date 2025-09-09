using Global.Domain.ViewModels;
using MediatR;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Queries.Bank
{
    public class BankGridQuery : IRequest<PaginatedResponse<BankVM>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
    }
}

