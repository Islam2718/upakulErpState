using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace MF.Domain.ViewModels
{
    public class BankAccountChequeVM
    {
        public int BankAccountChequeId { get; set; }
        public int? BankAccountMappingId { get; set; }
        public string? ChequeNumberPrefix { get; set; }
        public int? ChequeNumberFrom { get; set; }
        public int? ChequeNumberTo { get; set; }
    }

}
