using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain.ViewModel
{
    public class BudgetEntryComponentVM
    {
        public string FinancialYear { get; set; }
        public int OfficeId { get; set; }
        public int ParentId { get; set; }
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string? labelShow { get; set; }
        public int? NoOfStaff { get; set; }
        public int? NoOfPiece { get; set; }
        public int? ComponentNoOfDay { get; set; }
        public decimal ComponentPerAmount { get; set; }
        public decimal ComponentTotalAmount { get; set; }
        public int? NoOfGratuity { get; set; }
        public decimal? Basic1 { get; set; }
        public decimal? Other1 { get; set; }
        public decimal? Basic2 { get; set; }
        public decimal? Other2 { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? PF { get; set; }
        public decimal? Gratuity { get; set; }
        public decimal? MedicalAllowance { get; set; }

    }
}
