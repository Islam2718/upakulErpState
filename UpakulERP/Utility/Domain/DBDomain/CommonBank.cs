using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utility.Domain.DBDomain
{
    [Table("Bank", Schema = "dbo")]
    public class CommonBank
    {
        [Key]
        public int BankId { get; set; }
        public string BankType { get; set; }
        public string BankShortCode { get; set; }
        public string BankName { get; set; }
        public bool IsActive { get; set; }=true;
    }
}
