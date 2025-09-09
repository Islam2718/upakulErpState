using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace MF.Domain.Models
{
    [Table("BankAccountChequeMaster", Schema = "dbo")]
    public class BankAccountCheque : EntityBase
    {
        [Key]
        public int BankAccountChequeId { get; set; }
        public int? BankAccountMappingId { get; set; }
        public string? ChequeNumberPrefix { get; set; }
        public string? ChequeNumberFrom { get; set; }
        public string? ChequeNumberTo { get; set; }
    }
}
