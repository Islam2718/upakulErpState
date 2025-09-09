using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MF.Domain.Models.Loan
{
    [Table("LoanApproval", Schema = "loan")]
    public class LoanApproval
    {
        [Key]
        public int Level { get; set; }
        public string ApprovalType { get; set; }
        public int DesignationId { get; set; }
        public int StartingValueAmount { get; set; }
    }
}
