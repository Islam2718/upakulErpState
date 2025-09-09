using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Application.Features.DBOrders.Queries.BudgetComponent
{
    public class BudgetComponentVM
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string ComponentName { get; set; }
        public string? LabelShow { get; set; }
        public bool? IsMedical { get; set; }
        public bool? IsDesignation { get; set; }
    }

}
