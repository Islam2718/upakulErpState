using System.ComponentModel.DataAnnotations;
using Utility.Constants;
using Utility.Domain;
using Utility.Extensions;

namespace Project.Application.Contacts.Enums
{
    public enum TargetTypeEnum
    {
        Monthly= '1',
        [Display(Name = "3 Monthly")]
        Month_3='3',
        [Display(Name = "6 Monthly")]
        Month_6 = '6',
        [Display(Name = "Yearly")]
        yearly = 'Y',
    }
    public class TargetType
    {
        public List<CustomSelectListItem> GetTargetTypeDropDown()
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "" });
            list.AddRange(Enum.GetValues(typeof(TargetTypeEnum))
                .Cast<TargetTypeEnum>()
                .Select(d => new CustomSelectListItem
                {
                    Text = d.GetEnumDisplayName(),
                    Value = Convert.ToChar(d).ToString(),
                }).ToList());
            return list;
        }
    }
}
