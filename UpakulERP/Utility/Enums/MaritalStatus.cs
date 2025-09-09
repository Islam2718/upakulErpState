using Utility.Constants;
using Utility.Domain;

namespace Utility.Enums
{
    public class MaritalStatus
    {
        public const string divorced = "D";
        public const string married = "M";
        public const string single = "S";
        public const string widowed = "W";

        public enum MaritalStatusEnum
        {
            Married,
            Single,
            Divorced,
            Widowed,
        }
        public List<CustomSelectListItem> GetMaritalStatusDropDown(bool allowSingle,string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (string.IsNullOrEmpty(value) ? true : false) });
            list.AddRange(Enum.GetValues(typeof(MaritalStatusEnum))
                .Cast<MaritalStatusEnum>()
                .Where(x=> !allowSingle? x.ToString()!= MaritalStatusEnum.Single.ToString():x==x)
                .Select(d => new CustomSelectListItem
                {
                    Text = d.ToString(),
                    Value = (d.ToString() == MaritalStatusEnum.Divorced.ToString() ? divorced : d.ToString() == MaritalStatusEnum.Married.ToString() ? married : d.ToString() == MaritalStatusEnum.Single.ToString() ? single : widowed),
                    Selected = ((d.ToString() == MaritalStatusEnum.Divorced.ToString() ? divorced : d.ToString() == MaritalStatusEnum.Married.ToString() ? married : d.ToString() == MaritalStatusEnum.Single.ToString() ? single : widowed) == value ? true : false)
                }).ToList());
            return list;
        }
    }
}
