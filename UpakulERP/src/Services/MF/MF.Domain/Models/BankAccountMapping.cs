using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace MF.Domain.Models
{
    [Table("BankAccountMapping", Schema = "dbo")]
    public class BankAccountMapping : EntityBase
    {
        [Key]
        public int BankAccountMappingId { get; set; }
        public int? BankId { get; set; }
        public int? OfficeId { get; set; }
        public string BranchName { get; set; }
        public string RoutingNo { get; set; }
        public string BranchAddress { get; set; }
        public string? BankAccountName { get; set; }
        public string? BankAccountNumber { get; set; }
        public int? AccountId { get; set; }
    }
}
