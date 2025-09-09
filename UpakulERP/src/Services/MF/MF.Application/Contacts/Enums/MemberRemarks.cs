using System.ComponentModel.DataAnnotations;
using Utility.Constants;
using Utility.Domain;

namespace MF.Application.Contacts.Enums
{
    public class MemberRemarks
    {
        public enum MemberRemarkEnum
        {
            [Display(Name = "Freedom Fighter")]
            Freedom_Fighter = 1,
            [Display(Name = "Freedom Fighter Family Member")]
            Freedom_Fighter_Family_Member = 2,
            [Display(Name = "Disable Freedom Fighter")]
            Disable_Freedom_Fighter = 3,
            [Display(Name = "Disable")]
            Disable = 4,
            [Display(Name = "Tribal")]
            Tribal = 5,
            [Display(Name = "Others")]
            Others = 6,
        }
        public List<CustomSelectListItem> GetMemberRemarksDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = string.IsNullOrEmpty(value) ? true : false });
            list.AddRange(Enum.GetValues(typeof(MemberRemarkEnum))
                .Cast<MemberRemarkEnum>()
            .Select(d => new CustomSelectListItem
            {
                Text = d.ToString(),
                Value = ((int)d).ToString(),
                Selected = ((int)d).ToString() == value ? true : false
            }).ToList());
            return list;
        }
    }
}
