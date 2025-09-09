using Utility.Domain;

namespace MF.Application.Contacts.Enums
{
    public class GroupType
    {
        public const string GroupType_Male = "M";
        public const string GroupType_Female = "F";
        public enum SamityTypeEnum
        {
            Male,
            Female
        }

        public List<CustomSelectListItem> GetGroupTypeDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            //list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (string.IsNullOrEmpty(value) ? true : false) });
            list.AddRange(Enum.GetValues(typeof(SamityTypeEnum))
                .Cast<SamityTypeEnum>()
                .Select(d => new CustomSelectListItem
                {
                    Text = d.ToString(),
                    Value = d.ToString() == SamityTypeEnum.Male.ToString() ? GroupType_Male : d.ToString() == SamityTypeEnum.Female.ToString() ? GroupType_Female : "",
                    Selected = (d.ToString() == SamityTypeEnum.Male.ToString() ? GroupType_Male : d.ToString() == SamityTypeEnum.Female.ToString() ? GroupType_Female : "") == value ? true : false
                }).ToList());
            return list;
        }
    }
}
