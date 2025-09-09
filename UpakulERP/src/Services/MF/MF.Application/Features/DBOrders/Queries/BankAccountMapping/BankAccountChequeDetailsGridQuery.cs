using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Domain.ViewModels;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.BankAccountMapping
{
    public class BankAccountChequeDetailsGridQuery : IRequest<PaginatedResponse<BankAccountChequeDetailsVM>>
    {
        public int? OfficeId { get; set; }
        public int? BankAccountMappingId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
    }

}
