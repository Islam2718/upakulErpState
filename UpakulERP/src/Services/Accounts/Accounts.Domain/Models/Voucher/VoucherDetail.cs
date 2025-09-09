using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;
namespace Accounts.Domain.Models.Voucher
{
    [Table("VoucherDetails", Schema = "acc")]
    public class VoucherDetail : EntityBase
    {
        [Key]
        public long Id { get; set; }
        public long MasterId { get; set; }
        public int AccountId { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public string? Naration { get; set; }
        public string? ReferenceNo { get; set; }
        public int? OfficeId { get; set; }
        public int? ProjectId { get; set; }
    }
}
