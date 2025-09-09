using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Domain.Models;
using MF.Domain.Models.View;
using MF.Domain.ViewModels;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.BankAccountMapping
{
    public class BankAccountMappingGridQuery : IRequest<PaginatedResponse<BankAccountMappingVM>>
    { 
        public int? OfficeId { get; set; }
        public int? logedInOfficeId { get; set; }        
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
    }
}
