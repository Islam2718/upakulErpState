using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;

namespace Accounts.Domain.Models.Voucher
{
    [Table("VoucherMaster", Schema ="acc")]
    public class VoucherMaster : EntityBase
    {
        [Key]
        public long MasterId { get; set; }
        public string VoucherNo { get; set; }
        public DateTime VoucherDate { get; set; }
        public string VoucherType { get; set; }
        public string? VoucherNature { get; set; }
        public string? Naration { get; set; }
        public int? OfficeId { get; set; }
        public int? ProjectId { get; set; }
    }
}
