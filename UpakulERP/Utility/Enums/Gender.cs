using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;
using Utility.Domain;
using static Utility.Enums.MaritalStatus;

namespace Utility.Enums
{
    public class Gender
    {
        public const string male = "M";
        public const string female = "F";
        public const string others = "O";
        public const string transgender = "T";

        public enum GenderEnum
        {
            Male,
            Female,
            Transgender,
            Others
        }
        public List<CustomSelectListItem> GetGenderDropDown(string value="")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (string.IsNullOrEmpty(value) ? true : false) });
            list.AddRange(Enum.GetValues(typeof(GenderEnum))
                .Cast<GenderEnum>()
                .Select(d => new CustomSelectListItem
                {
                    Text = d.ToString(),
                    Value = (d.ToString() == GenderEnum.Male.ToString() ? male : d.ToString() == GenderEnum.Female.ToString() ? female : d.ToString() == GenderEnum.Transgender.ToString() ? transgender : others),
                    Selected = ((d.ToString() == GenderEnum.Male.ToString() ? male : d.ToString() == GenderEnum.Female.ToString() ? female : d.ToString() == GenderEnum.Transgender.ToString() ? transgender : others) == value ? true : false)
                }).ToList());
            return list;
        }
    }
}
