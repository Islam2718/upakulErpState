using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounts.Application.Features.DBOrders.Queries.BudgetComponent;
using Accounts.Domain.Models;
using Accounts.Domain.ViewModel;
using MediatR;

namespace Accounts.Application.Features.DBOrders.Queries.BudgetEntry
{
    public class GetRowQuery : IRequest<List<BudgetComponentData>>
    {
        public string FinancialYear { get; set; }
        public int OfficeId { get; set; }
        public int ComponentParentId { get; set; }
       // public int ComponentId { get; set; }


        public GetRowQuery(string FinancialYear, int OfficeId, int ComponentParentId) //, int ComponentId
        {
            this.FinancialYear = FinancialYear;
            this.OfficeId = OfficeId;
            this.ComponentParentId = ComponentParentId;
            //this.ComponentId = ComponentId;
        }
    }
}
