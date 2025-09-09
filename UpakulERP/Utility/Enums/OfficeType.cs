using System.ComponentModel.DataAnnotations;
using Utility.Constants;
using Utility.Domain;
using Utility.Extensions;

namespace Utility.Enums
{
    public class OfficeType
    {
        public enum OfficeTypeEnum
        {
            [Display(Name = "Principal office")]
            Principal = 1,
            Project = 2,
            [Display(Name = "Zonal office")]
            Zonal = 3,
            [Display(Name = "Regional office")]
            Regional = 4,
            [Display(Name = "Area office")]
            Area = 5,
            [Display(Name = "Branch office")]
            Branch = 6,
        }
        public List<CustomSelectListItem> GetOfficeTypeDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (string.IsNullOrEmpty(value) ? true : false) });
            list.AddRange(Enum.GetValues(typeof(OfficeTypeEnum))
                .Cast<OfficeTypeEnum>()
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
