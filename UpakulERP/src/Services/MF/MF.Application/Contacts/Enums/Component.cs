using System.ComponentModel.DataAnnotations;
using Utility.Domain;
using Utility.Extensions;
namespace MF.Application.Contacts.Enums
{
  public  class Component
    {
        public const string Term_Savings_FDR = "F";
        public const string Term_Savings_DPS = "D";
        public const string Security_Saving = "S";
        public const string Volenter_Saving = "V";
        public const string Loan = "L";
        public enum ComponentTypeEnum
        {
            [Display(Name = "Loan")]
            Loan,
            [Display(Name = "Security (Compulsory) Saving")]
            Security_Saving,
            [Display(Name = "Volenter (Open) Saving")]
            Volenter_Saving,
            [Display(Name = "Term Savings - DPS")]
            Term_Savings_DPS,
            [Display(Name = "Term Savings - FDR")]
            Term_Savings_FDR,
        }

        public List<CustomSelectListItem> GetComponentTypeDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            //list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (string.IsNullOrEmpty(value) ? true : false) });
            list.AddRange(Enum.GetValues(typeof(ComponentTypeEnum))
                .Cast<ComponentTypeEnum>()
                .Select(d => new CustomSelectListItem
                {
                    Text = d.GetEnumDisplayName(),
                    Value = d.ToString() == ComponentTypeEnum.Loan.ToString() ? Loan 
                    : d.ToString() == ComponentTypeEnum.Security_Saving.ToString() ? Security_Saving
                    :d.ToString() == ComponentTypeEnum.Volenter_Saving.ToString() ? Volenter_Saving
                    : d.ToString() == ComponentTypeEnum.Term_Savings_DPS.ToString() ? Term_Savings_DPS
                    : d.ToString() == ComponentTypeEnum.Term_Savings_FDR.ToString() ? Term_Savings_FDR
                    : "",
                    Selected = (d.ToString() == ComponentTypeEnum.Loan.ToString()? Loan:"") == value ? true : false
                }).ToList());
            return list;
        }
    }
}
