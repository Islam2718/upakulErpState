using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounts.Domain.Models.Voucher
{
    [Table("OfficeXHeadAssign", Schema = "acc")]
    public class OfficeXHeadAssign
    {
        [Key]
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public int AccountId { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
