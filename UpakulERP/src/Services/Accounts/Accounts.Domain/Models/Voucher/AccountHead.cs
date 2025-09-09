
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;

namespace Accounts.Domain.Models.Voucher
{
    [Table("AccountHead", Schema = "acc")]
    public class AccountHead : EntityBase
    {
        [Key]
        public int AccountId { get; set; }
        public string HeadCode { get; set; }
        public string HeadName { get; set; }
        public bool IsTransactable { get; set; }
        public int? ParentId { get; set; }
    }
}
