using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utility.Enums.Days;
using Utility.Constants;
using Utility.Domain;
using Utility.Extensions;

namespace Utility.Enums
{
    public class BloodGroup
    {
        public enum BloodGroupEnum
        {
            [Display(Name = "A+")]
            APositive,
            [Display(Name = "A-")]
            ANegative,
            [Display(Name = "B+")]
            BPositive,
            [Display(Name = "B-")]
            BNegative,
            [Display(Name = "AB+")]
            ABPositive,
            [Display(Name = "AB-")]
            ABNegative,
            [Display(Name = "O+")]
            OPositive,
            [Display(Name = "O-")]
            ONegative
        }

        public List<CustomSelectListItem> GetBloodGroupDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (string.IsNullOrEmpty(value) ? true : false) });
            list.AddRange(  Enum.GetValues(typeof(BloodGroupEnum))
                .Cast<BloodGroupEnum>()
            .Select(d => new CustomSelectListItem
            {
                Text = EnumDisplayName.GetEnumDisplayName(d),
                Value = EnumDisplayName.GetEnumDisplayName(d),
                Selected = (EnumDisplayName.GetEnumDisplayName(d) == value ? true : false)
            }).ToList());
            return list;
        }
    }
}
