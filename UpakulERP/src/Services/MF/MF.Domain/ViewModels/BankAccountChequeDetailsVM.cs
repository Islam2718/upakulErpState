using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Domain.ViewModels
{
    public class BankAccountChequeDetailsVM
    {
        public int BankAccountChequeIDetailsId { get; set; }
        public int? BankAccountChequeId { get; set; }
        public string? ChequeNumber { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }

        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? BankBranchName { get; set; }

        public int? ConsumedBy { get; set; }
        public DateTime? ConsumedDate { get; set; }

    }

}
