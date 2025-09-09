using Utility.Constants;
using Utility.Domain;

namespace Utility.Enums.HRM
{
   public class EmployeeType
    {
        public const string permanent = "P";
        public const string contractual = "C";
        public const string casualLabor = "K";
        public const string dailyWorker = "D";
        public const string internship = "I";
        public const string provisional = "V";


        public enum EmployeeTypeEnum
        {
            Permanent,
            Contractual,
            CasualLabor,
            Internship,
            Provisional,
            DailyWorker
        }

        public List<CustomSelectListItem> GetEmployeeTypeDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = string.IsNullOrEmpty(value) ? true : false });
            list.AddRange(Enum.GetValues(typeof(EmployeeTypeEnum))
                .Cast<EmployeeTypeEnum>()
                .Select(d => new CustomSelectListItem
                {
                    Text = d.ToString(),
                    Value = d.ToString() == EmployeeTypeEnum.Permanent.ToString() ? permanent : d.ToString() == EmployeeTypeEnum.Contractual.ToString() ? contractual : d.ToString() == EmployeeTypeEnum.CasualLabor.ToString() ? casualLabor : d.ToString() == EmployeeTypeEnum.Internship.ToString() ? internship : d.ToString() == EmployeeTypeEnum.Provisional.ToString() ? provisional : dailyWorker,
                    Selected = (d.ToString() == EmployeeTypeEnum.Permanent.ToString() ? permanent : d.ToString() == EmployeeTypeEnum.Contractual.ToString() ? contractual : d.ToString() == EmployeeTypeEnum.CasualLabor.ToString() ? casualLabor : d.ToString() == EmployeeTypeEnum.Internship.ToString() ? internship : d.ToString() == EmployeeTypeEnum.Provisional.ToString() ? provisional : dailyWorker) == value ? true : false
                }).ToList());
            return list;
        }
    }
}
