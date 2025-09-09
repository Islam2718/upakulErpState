using System.ComponentModel.DataAnnotations;
using Utility.Constants;
using Utility.Domain;
using Utility.Extensions;

namespace Utility.Enums.HRM
{
    public class LeaveCategory
    {
        public const string casual = "CL";
        public const string sick = "SL";
        public const string annual = "AL";
        public const string maternity = "ML";
        public const string paternity = "PL";
        public const string leave_Without_Pay = "LWP";
        public enum LeaveCategoryEnum
        {
            [Display(Name = "Casual Leave")]
            Casual,
            [Display(Name = "Sick Leave")]
            Sick,
            [Display(Name = "Annual Leave")]
            Annual,
            [Display(Name = "Maternity Leave")]
            Maternity,
            [Display(Name = "Paternity Leave")]
            Paternity,
            [Display(Name = "Leave Without Pay")]
            Leave_Without_Pay,

        }
        public List<CustomSelectListItem> GetLeaveCategoryDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (string.IsNullOrEmpty(value) ? true : false) });
            list.AddRange(Enum.GetValues(typeof(LeaveCategoryEnum))
                .Cast<LeaveCategoryEnum>()
                .Select(d => new CustomSelectListItem
                {
                    Text = EnumDisplayName.GetEnumDisplayName(d),
                    Value = (d.ToString() == LeaveCategoryEnum.Casual.ToString() ? casual 
                    : d.ToString() == LeaveCategoryEnum.Sick.ToString() ? sick 
                    : d.ToString() == LeaveCategoryEnum.Annual.ToString() ? annual
                    : d.ToString() == LeaveCategoryEnum.Maternity.ToString() ? maternity
                    : d.ToString() == LeaveCategoryEnum.Paternity.ToString() ? paternity
                    : leave_Without_Pay),
                    Selected = ((d.ToString() == LeaveCategoryEnum.Casual.ToString() ? casual
                    : d.ToString() == LeaveCategoryEnum.Sick.ToString() ? sick
                    : d.ToString() == LeaveCategoryEnum.Annual.ToString() ? annual
                    : d.ToString() == LeaveCategoryEnum.Maternity.ToString() ? maternity
                    : d.ToString() == LeaveCategoryEnum.Paternity.ToString() ? paternity
                    : leave_Without_Pay) == value ? true : false)
                }).ToList());
            return list;
        }
    }
}
