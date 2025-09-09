using System.ComponentModel.DataAnnotations;
using Utility.Constants;
using Utility.Domain;
using Utility.Extensions;

namespace Utility.Enums
{
    public class BankType
    {
        public const string CoreBank = "C";
        public const string AgentBank = "A";
        public const string MobileBanking = "M";

        public enum BankTypeEnum
        {

            [Display(Name = "Core Bank")]
            CoreBank,
            [Display(Name = "Agent Bank")]
            AgentBank,
            [Display(Name = "Mobile Banking")]
            MobileBanking
        }

        public List<CustomSelectListItem> GetBankTypeDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (string.IsNullOrEmpty(value) ? true : false) });
            list.AddRange(Enum.GetValues(typeof(BankTypeEnum))
                .Cast<BankTypeEnum>()
                .Select(d => new CustomSelectListItem
                {
                    Text = EnumDisplayName.GetEnumDisplayName(d),
                    Value = (d.ToString() == BankTypeEnum.CoreBank.ToString() ? CoreBank : d.ToString() == BankTypeEnum.AgentBank.ToString() ? AgentBank : MobileBanking),
                    Selected = ((d.ToString() == BankTypeEnum.CoreBank.ToString() ? CoreBank : d.ToString() == BankTypeEnum.AgentBank.ToString() ? AgentBank : MobileBanking) == value ? true : false)
                }).ToList());
            return list;
        }
    }
}
