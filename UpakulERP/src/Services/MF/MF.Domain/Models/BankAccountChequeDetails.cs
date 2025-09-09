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
    [Table("BankAccountChequeDetails", Schema = "dbo")]
    public class BankAccountChequeDetails
    {
        [Key]
        public int BankAccountChequeIDetailsId { get; set; }
        public int? BankAccountChequeId { get; set; }        
        public string? ChequeNumber { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }

        public int? ConsumedBy { get; set; }
        public DateTime? ConsumedDate { get; set; }
    }
}
