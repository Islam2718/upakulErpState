

namespace MF.Domain.ViewModels.Collection
{
    public class EmployeeXGroupCollectionVM
    {
        public string? DayName { get; set; }
        public string? MemberStatus { get; set; }
        public string? GroupName {  get; set; }
        public int? GroupId { get; set; }
        public string? Employee { get; set; }
        public string? CompulsoryComponent { get; set; }
        public int? NoOfCompulsorySaving { get; set; }

        public string? VolenterComponent { get; set; }
        public int? NoOfVolenterSaving { get; set; }

        public string? GeneralComponent { get; set; }
        public int? NoOfGeneralLoan { get; set; }

        public string? ProjectComponent { get; set; }
        public int? NoOfProjectLoan { get; set; }
    }
}
