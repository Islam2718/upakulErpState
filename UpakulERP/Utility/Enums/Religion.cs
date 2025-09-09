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
    public class Religion
    {
        public const string islam = "I";
        public const string hinduism = "H";
        public const string christianity = "C";
        public const string buddhism = "B";
        public const string irreligion = "R";
        public const string others = "O";
        public enum ReligionEnum
        {
            Islam,
            Hinduism,
            Christianity,
            Buddhism,
            Irreligion,
            Others
        }
        public List<CustomSelectListItem> GetReligionDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (string.IsNullOrEmpty(value) ? true : false) });
            list.AddRange(Enum.GetValues(typeof(ReligionEnum))
                .Cast<ReligionEnum>()
                .Select(d => new CustomSelectListItem
                {
                    Text = d.ToString(),
                    Value = (d.ToString() == "Islam" ? islam : d.ToString() == "Hinduism" ? hinduism : d.ToString() == "Christianity" ? christianity : d.ToString() == "Buddhism" ? buddhism : d.ToString() == "Irreligion" ? irreligion : others),
                    Selected = ((d.ToString() == "Islam" ? islam : d.ToString() == "Hinduism" ? hinduism : d.ToString() == "Christianity" ? christianity : d.ToString() == "Buddhism" ? buddhism : d.ToString() == "Irreligion" ? irreligion : others) == value ? true : false)
                }).ToList());
            return list;
        }
    }
}
