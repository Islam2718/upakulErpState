

namespace MF.Domain.ViewModels.Collection
{
    public class GroupXMemberCollectionVM
    {
        public string? DayName { get; set; }
        public string? MemberCode { get; set; }
        public string? MemberName { get; set; }
        public string? MemberStatus { get; set; }
        public string? GroupName { get; set; }
       
        public string? Employee { get; set; }
        public string? CompulsoryComponent { get; set; }
        public decimal? CompulsoryAmount { get; set; }
        public decimal? CompulsorySavingCollectionAmount { get; set; }
        public decimal? CompulsorySavingRefund { get; set; }
        public long? CompulsorySavingSummaryId { get; set; }

        public string? VolenterComponent { get; set; }
        public decimal? VolenterAmount { get; set; }
        public decimal? VolenterSavingCollectionAmount { get; set; }
        public decimal? VolenterSavingRefund { get; set; }
        public long? VolenterSavingSummaryId { get; set; }

        public string? GeneralComponent { get; set; }
        public int? GeneralComponentId { get; set; }
        public decimal? GeneralDue { get; set; }
        public int? GeneralLoanCollectionAmount { get; set; }
        public int? GeneralLoanRebateAmount { get; set; }
        public long? GeneralLoanSummaryId { get; set; }


        public string? ProjectComponent { get; set; }
        public int? ProjectComponentId { get; set; }
        public decimal? ProjectDue { get; set; }
        public int? ProjectLoanCollectionAmount { get; set; }
        public int? ProjectLoanRebateAmount { get; set; }
        public long? ProjectLoanSummaryId { get; set; }

    }
}
