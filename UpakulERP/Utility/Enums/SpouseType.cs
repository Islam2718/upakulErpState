using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utility.Enums.MaritalStatus;
using Utility.Constants;
using Utility.Domain;

namespace Utility.Enums
{
    public class SpouseType
    {
        public const string husband = "H";
        public const string wife = "W";
        public enum SpouseTypeEnum
        {
            Husband,
            Wife
        }
        public List<CustomSelectListItem> GetSpouseTypeDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (string.IsNullOrEmpty(value) ? true : false) });
            list.AddRange(Enum.GetValues(typeof(SpouseTypeEnum))
                .Cast<SpouseTypeEnum>()
                .Select(d => new CustomSelectListItem
                {
                    Text = d.ToString(),
                    Value = (d.ToString() == "Husband" ? husband :  wife),
                    Selected = ((d.ToString() == "Husband" ? husband :wife ) == value ? true : false)
                }).ToList());
            return list;
        }
    }
}
