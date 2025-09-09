using Utility.Constants;
using Utility.Domain;

namespace Utility.Enums.HRM
{
    public class EmployeeStatus
    {
        public const string active = "A";
        public const string inActive = "I";
        //public const string delete = "D";
        public const string suspension = "S";
        public const string salaryHeldup = "H";
        public const string dismissal = "D";
        public const string terminated = "T";
        public const string resigned = "R";
        public const string finalSettlement = "F";


        public enum EmployeeStatusEnum
        {
            Active,
            InActive,
            //Delete,
            Suspension,
            SalaryHeldUp,
            Dismissal,
            Terminated,
            Resigned,
            FinalSettlement
        }

        public List<CustomSelectListItem> GetEmployeeStatusDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = string.IsNullOrEmpty(value) ? true : false });
            list.AddRange(Enum.GetValues(typeof(EmployeeStatusEnum))
                .Cast<EmployeeStatusEnum>()
                .Select(d => new CustomSelectListItem
                {
                    Text = d.ToString(),
                    Value = d.ToString() == EmployeeStatusEnum.Active.ToString() ? active : d.ToString() == EmployeeStatusEnum.InActive.ToString() ? inActive : d.ToString() == EmployeeStatusEnum.Suspension.ToString() ? suspension : d.ToString() == EmployeeStatusEnum.SalaryHeldUp.ToString() ? salaryHeldup : d.ToString() == EmployeeStatusEnum.Dismissal.ToString() ? dismissal : d.ToString() == EmployeeStatusEnum.Terminated.ToString() ? terminated : d.ToString() == EmployeeStatusEnum.Resigned.ToString() ? resigned : finalSettlement,
                    Selected = (d.ToString() == EmployeeStatusEnum.Active.ToString() ? active : d.ToString() == EmployeeStatusEnum.InActive.ToString() ? inActive : d.ToString() == EmployeeStatusEnum.Suspension.ToString() ? suspension : d.ToString() == EmployeeStatusEnum.SalaryHeldUp.ToString() ? salaryHeldup : d.ToString() == EmployeeStatusEnum.Dismissal.ToString() ? dismissal : d.ToString() == EmployeeStatusEnum.Terminated.ToString() ? terminated : d.ToString() == EmployeeStatusEnum.Resigned.ToString() ? resigned : finalSettlement) == value ? true : false
                }).ToList());
            return list;
        }

        //public List<SelectListItem> GetEmployeeStatusDropDown(string value = "")
        //{
        //    var list = new List<SelectListItem>();
        //    list.Add(new SelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (string.IsNullOrEmpty(value) ? true : false) });
        //    list.AddRange(Enum.GetValues(typeof(EmployeeStatusEnum))
        //        .Cast<EmployeeStatusEnum>()
        //    .Select(d => new SelectListItem
        //    {
        //        Text = EnumDisplayName.GetEnumDisplayName(d),
        //        Value = d.ToString(),
        //        Selected = (d.ToString() == value ? true : false)
        //    }).ToList());
        //    return list;
        //}
    }
}
