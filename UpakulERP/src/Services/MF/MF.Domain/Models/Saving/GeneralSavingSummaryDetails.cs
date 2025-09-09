using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MF.Domain.Models.Saving
{
    [Table("GeneralSummaryDetails", Schema = "saving")]
    public class GeneralSavingSummaryDetails
    {
        [Key]
        public long Id { get; set; }
        public long GeneralSummaryId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public decimal? Receipt { get; set; }
        public decimal? Payment { get; set; }


        public GeneralSavingSummary Summary { get; set; }
    }
}
