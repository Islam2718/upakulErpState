using Utility.Constants;
using Utility.Domain;

namespace MF.Application.Contacts.Enums
{
    public class DisbursementRequestActivities
    {
        public const string Approved = "A";
        public const string Refused = "F";
        public const string Rejected = "R";
        public const string Heldup = "H";
        public enum DisbursementRequestActivitiesEnum
        {
            Approved,
            Refused,
            Rejected,
            Heldup
        }
        public List<CustomSelectListItem> GetDisbursementRequestActivitiesDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = string.IsNullOrEmpty(value) ? true : false });
            list.AddRange(Enum.GetValues(typeof(DisbursementRequestActivitiesEnum))
                .Cast<DisbursementRequestActivitiesEnum>()
                .Select(d => new CustomSelectListItem
                {
                    Text = d.ToString(),
                    Value = d.ToString() == DisbursementRequestActivitiesEnum.Approved.ToString() ? Approved
                    : d.ToString() == DisbursementRequestActivitiesEnum.Refused.ToString() ? Refused
                    : d.ToString() == DisbursementRequestActivitiesEnum.Rejected.ToString() ? Rejected
                    : Heldup,
                    Selected = (d.ToString() == DisbursementRequestActivitiesEnum.Approved.ToString() ? Approved
                    : d.ToString() == DisbursementRequestActivitiesEnum.Refused.ToString() ? Refused
                    : d.ToString() == DisbursementRequestActivitiesEnum.Rejected.ToString() ? Rejected
                    : Heldup) == value ? true : false
                }).ToList());
            return list;
        }
    }
}
