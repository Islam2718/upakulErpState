using System.ComponentModel.DataAnnotations;
using Utility.Constants;
using Utility.Domain;

namespace MF.Application.Contacts.Enums
{
    public class MemberEducation
    {
        public enum MemberEducationEnum
        {
            [Display(Name = "Pre-Primary")]
            Pre_Primary = 1,
            [Display(Name = "Primary")]
            Primary = 2,
            [Display(Name = "Secondary")]
            Secondary = 3,
            [Display(Name = "Higher Secondary")]
            Higher_Secondary = 4,
            [Display(Name = "Graduation")]
            Graduation = 5,
            [Display(Name = "Post-Graduation")]
            Post_Graduation = 6,
            [Display(Name = "Others")]
            Others = 7
        }
        public List<CustomSelectListItem> GetMemberEducationDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = string.IsNullOrEmpty(value) ? true : false });
            list.AddRange(Enum.GetValues(typeof(MemberEducationEnum))
                .Cast<MemberEducationEnum>()
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
