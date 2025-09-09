using Utility.Constants;
using Utility.Domain;

namespace Utility.Enums
{
    public class Days
    {
        public const string saturday_short_form = "Sat";
        public const string sunday_short_form = "Sun";
        public const string monday_short_form = "Mon";
        public const string tuesday_short_form = "Tue";
        public const string wednesday_short_form = "Wed";
        public const string thursday_short_form = "Thu";
        public const string friday_short_form = "Fri";
        public enum DaysEnum
        {
            Saturday = 7,
            Sunday = 1,
            Monday = 2,
            Tuesday = 3,
            Wednesday = 4,
            Thursday = 5,
            Friday = 6
            
        }
        public List<CustomSelectListItem> GetDaysDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (string.IsNullOrEmpty(value) ? true : false) });
            list.AddRange(Enum.GetValues(typeof(DaysEnum))
                .Cast<DaysEnum>()
            .Select(d => new CustomSelectListItem
            {
                Text = d.ToString(),
                Value = ((int)d).ToString(),
                Selected = (((int)d).ToString() == value ? true : false)
            }).ToList());
            return list;
        }
    }
}
