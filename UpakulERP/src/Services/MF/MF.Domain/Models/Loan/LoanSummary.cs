using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;

namespace MF.Domain.Models.Loan
{
    [Table("Summary", Schema = "loan")]
    public class LoanSummary: EntityBase
    {
        [Key]
        public long SummaryId { get; set; }
        public long LoanApplicationId { get; set; }
        public int OfficeId { get; set; }
        public int MemberId { get; set; }
        public int GroupId { get; set; }
        public int LoanPurposeId { get; set; }
        public int LoanComponentId { get; set; }
        public string PaymentType {  get; set; }
        public int? BankAccountMappingId {  get; set; }
        public string? ChequeNo { get; set; }
        public string? ReferenceNo {  get; set; }
        public string PaymentFrequency { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime DisburseDate { get; set; }
        public int GracePeriod { get; set; }
        public DateTime ExpireDate { get; set; }
        public int PrincipleAmount { get; set; }
        public decimal ServiceCharge { get; set; }
        public int? TotalOverDuePrincipal { get; set; }
        public decimal? TotalOverDueServiceCharge { get; set; }
        public int? TotalAdvancePrincipal { get; set; }
        public decimal? TotalAdvanceServiceCharge { get; set; }
        public decimal? TotalFine { get; set; }
        public decimal? TotalFineCollection { get; set; }
        public int? TotalPrincipalCollection { get; set; }
        public decimal? TotalServiceChargeCollection { get; set; }
        public decimal? TotalServiceChargeRebate { get; set; }
        public long? CompulsoryGeneralSavingSummaryId { get; set; }
        public long? OpeningGeneralSavingSummaryId { get; set; }
    }
}
