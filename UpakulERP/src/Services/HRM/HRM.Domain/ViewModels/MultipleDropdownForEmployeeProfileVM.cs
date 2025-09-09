using Utility.Domain;

namespace HRM.Domain.ViewModels
{
    public class MultipleDropdownForEmployeeProfileVM
    {
        public List<CustomSelectListItem> department { get; set; }
        public List<CustomSelectListItem> designation { get; set; }
        public List<CustomSelectListItem> country { get; set; }
        public List<CustomSelectListItem> office { get; set; }
        public List<CustomSelectListItem> circular { get; set; }
        public List<CustomSelectListItem> bank { get; set; }
        public List<CustomSelectListItem> division { get; set; }
        public List<CustomSelectListItem> occupation { get; set; }

        // Enum
        public List<CustomSelectListItem> employeeType { get; set; }
        public List<CustomSelectListItem> employeeStatus { get; set; }
        public List<CustomSelectListItem> gender { get; set; }
        public List<CustomSelectListItem> religion { get; set; }
        public List<CustomSelectListItem> bloodGroup { get; set; }
        public List<CustomSelectListItem> maritalStatus { get; set; }
    }
}
