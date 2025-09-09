using Utility.Domain;

namespace MF.Domain.ViewModels
{
    public class MultipleDropdownForMemberProfileVM
    {
        public List<CustomSelectListItem> group { get; set; }
        public List<CustomSelectListItem> occupation { get; set; }
        public List<CustomSelectListItem> maritalStatus { get; set; }
        public List<CustomSelectListItem> gender { get; set; }
        public List<CustomSelectListItem> academicQualification { get; set; }
        public List<CustomSelectListItem> authorizedPerson { get; set; }
        public List<CustomSelectListItem> remarks { get; set; }
        public List<CustomSelectListItem> referenceMember { get; set; }
        public List<CustomSelectListItem> country { get; set; }
        public List<CustomSelectListItem> division { get; set; }
    }
}
