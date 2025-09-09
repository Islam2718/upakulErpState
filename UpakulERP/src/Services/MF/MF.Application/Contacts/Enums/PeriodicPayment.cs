using System.ComponentModel.DataAnnotations;
using Utility.Constants;
using Utility.Domain;
using Utility.Extensions;

namespace MF.Application.Contacts.Enums
{
    public class PeriodicPayment
    {
        public const string Weekly_Installments = "W";
        public const string Fortnight_Installments = "F";
        public const string Monthly_Installments = "M";
        public const string Quarterly_Installments = "Q";
        public const string Installments_every_six_months = "S";
        public const string Yearly_Installments = "Y";
        public const string Irregular_Installments = "I";

        public enum PeriodicPaymentEnum
        {
            [Display(Name = "Weekly Installments")]
            Weekly_Installments,
            [Display(Name = " Fortnight Installments")]
            Fortnight_Installments,
            [Display(Name = "Monthly Installments")]
            Monthly_Installments,
            [Display(Name = "Quarterly Installments/ Profit")]
            Quarterly_Installments,
            [Display(Name = "Installments/ Profit every six months")]
            Installments_every_six_months,
            [Display(Name = "Yearly")]
            Yearly_Installments,
            [Display(Name = "Irregular Installments")]
            Irregular_Installments,
        }

        public List<CustomSelectListItem> GetPeriodicPaymentDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = string.IsNullOrEmpty(value) ? true : false });
            list.AddRange(Enum.GetValues(typeof(PeriodicPaymentEnum))
                .Cast<PeriodicPaymentEnum>()
                .Select(d => new CustomSelectListItem
                {
                    Text = d.GetEnumDisplayName(),
                    Value = d.ToString() == PeriodicPaymentEnum.Weekly_Installments.ToString() ? Weekly_Installments
                    : d.ToString() == PeriodicPaymentEnum.Fortnight_Installments.ToString() ? Fortnight_Installments
                    : d.ToString() == PeriodicPaymentEnum.Monthly_Installments.ToString() ? Monthly_Installments
                    : d.ToString() == PeriodicPaymentEnum.Quarterly_Installments.ToString() ? Quarterly_Installments
                    : d.ToString() == PeriodicPaymentEnum.Installments_every_six_months.ToString() ? Installments_every_six_months
                    : d.ToString() == PeriodicPaymentEnum.Yearly_Installments.ToString() ? Yearly_Installments
                    : Irregular_Installments
                    ,
                    Selected = false
                }).Where(x=>x.Value!=Irregular_Installments).ToList());
            return list;
        }
    }
}
