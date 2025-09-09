using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;

namespace MF.Domain.Models.Saving
{
    [Table("GeneralSummary", Schema = "savings")]
    public class GeneralSavingSummary
    {
        [Key]
        public long GeneralSummaryId { get; set; }
        public int OfficeId { get; set; }
        public int MemberId { get; set; }
        public int GroupId { get; set; }
        public int ComponentId { get; set; }
        public decimal InterestRate { get; set; }
        public decimal? PrincipleAmount { get; set; }
        public decimal? ProfitAmount { get; set; }
        public decimal? NonMergeProfitAmount { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
