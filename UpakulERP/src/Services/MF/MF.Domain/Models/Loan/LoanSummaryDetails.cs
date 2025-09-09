using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;

namespace MF.Domain.Models.Loan
{
    [Table("SummaryDetails", Schema = "loan")]
    public class LoanSummaryDetail: EntityBase
    {
        public long Id { get; set; }
        public long SummaryId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int? PrincipalAmount { get; set; }
        public int? ServiceCharge { get; set; }
        public bool IsPosted { get; set; }
        public long? VoucherId { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
