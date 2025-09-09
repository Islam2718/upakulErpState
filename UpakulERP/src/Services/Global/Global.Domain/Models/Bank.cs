using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace Global.Domain.Models
{
    [Table("Bank", Schema = "dbo")]
    public class Bank : EntityBase
    {
        [Key]
        public int BankId { get; set; }
        public string BankType { get; set; }
        public string BankShortCode { get; set; }
        public string BankName {  get; set; }
    }
}
