using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Application.Features.DBOrders.Queries.BudgetEntry
{
    public class GetBudgetEntryComponent
    {
        public string? FinancialYear{ get; set; }
        public int? OfficeId{ get; set; }
        public int? ComponentParentId { get; set; }
        public int? ComponentId { get; set; }

    }
}
