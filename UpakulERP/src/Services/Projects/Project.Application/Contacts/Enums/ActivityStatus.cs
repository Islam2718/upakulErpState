using System.ComponentModel.DataAnnotations;
using Utility.Constants;
using Utility.Domain;
using Utility.Extensions;

namespace Project.Application.Contacts.Enums
{
    public enum ActivityStatusEnum
    {
        [Display(Name = "Not Start yet")]
        Not_Start_yet = 'N',
        [Display(Name = "Ongoing")]
        Ongoing = 'O',
        [Display(Name = "Completed")]
        yearly = 'C',
    }
    public class ActivityStatus
    {
        public List<CustomSelectListItem> GetActivityStatusDropDown()
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "" });
            list.AddRange(Enum.GetValues(typeof(ActivityStatusEnum))
                .Cast<ActivityStatusEnum>()
                .Select(d => new CustomSelectListItem
                {
                    Text = d.GetEnumDisplayName(),
                    Value = Convert.ToChar(d).ToString(),
                }).ToList());
            return list;
        }
    }
}
