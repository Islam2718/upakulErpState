using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utility.Enums.BloodGroup;
using Utility.Constants;
using Utility.Domain;
using Utility.Extensions;

namespace Utility.Enums
{
    public class GeoLocationType
    {
        public enum GeoLocationTypeEnum
        {
            Division = 1,
            District = 2,
            [Display(Name = "Thana/Upazila")]
            Thana_Upazila = 3,
            Union = 4,
            Village = 5,
        }
        public List<CustomSelectListItem> GetGeoLocationTypeDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (string.IsNullOrEmpty(value) ? true : false) });
            list.AddRange(Enum.GetValues(typeof(GeoLocationTypeEnum))
                .Cast<GeoLocationTypeEnum>()
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
