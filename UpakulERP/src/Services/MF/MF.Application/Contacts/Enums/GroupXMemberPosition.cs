using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;
using Utility.Domain;
using Utility.Extensions;

namespace MF.Application.Contacts.Enums
{
    public class GroupXMemberPosition
    {
        public enum GroupXMemberPositionEnum
        {
            [Display(Name = "Chairman")]
            Chairman = 1,
            [Display(Name = "Vice Chairman")]
            ViceChairman = 2,
            [Display(Name = "Secretary")]
            Secretary = 3,
            [Display(Name = "Financial Secretary")]
            FinancialSecretary = 4,
            [Display(Name = "Social Secretary")]
            SocialSecretary = 5
        }
        public List<CustomSelectListItem> GetGroupXMemberPositionDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            //list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (string.IsNullOrEmpty(value) ? true : false) });
            list.AddRange(Enum.GetValues(typeof(GroupXMemberPositionEnum))
                .Cast<GroupXMemberPositionEnum>()
            .Select(d => new CustomSelectListItem
            {
                Text = EnumDisplayName.GetEnumDisplayName(d),
                Value = ((int)d).ToString(),
                Selected = (((int)d).ToString() == value ? true : false)
            }).ToList());
            return list;
        }
    }
}
