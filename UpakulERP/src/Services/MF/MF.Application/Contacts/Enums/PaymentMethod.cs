using System.ComponentModel.DataAnnotations;
using Utility.Constants;
using Utility.Domain;
using Utility.Extensions;

namespace MF.Application.Contacts.Enums
{
    public class PaymentMethod
    {
        public const string Cash = "CAS";
        public const string Cheque = "CHQ";
        public const string Mobile_Financial_Service = "MFS";
        public const string Bank_Draft = "BAR";
        public const string Direct_transfer_Postal_payment_slip = "DIR";
        public const string Multiple = "MUL";
        public const string Other = "OTH";

        public enum PaymentMethodEnum
        {
            [Display(Name = "Cash")]
            Cash,
            [Display(Name = "Cheque")]
            Cheque,
            [Display(Name = "Mobile Financial Service")]
            Mobile_Financial_Service,
            [Display(Name = "Bank draft")]
            Bank_Draft,
            [Display(Name = "Direct transfer/ Postal payment slip")]
            Direct_transfer_Postal_payment_slip,
            [Display(Name = "Multiple")]
            Multiple,
            [Display(Name = "Other")]
            Other
        }

        public List<CustomSelectListItem> GetPaymentMethodDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = string.IsNullOrEmpty(value) ? true : false });
            list.AddRange(Enum.GetValues(typeof(PaymentMethodEnum))
                .Cast<PaymentMethodEnum>()
                .Select(d => new CustomSelectListItem
                {
                    Text = d.GetEnumDisplayName(),
                    Value = d.ToString() == PaymentMethodEnum.Cash.ToString() ? Cash
                    : d.ToString() == PaymentMethodEnum.Cheque.ToString() ? Cheque
                    : d.ToString() == PaymentMethodEnum.Mobile_Financial_Service.ToString() ? Mobile_Financial_Service
                    : d.ToString() == PaymentMethodEnum.Bank_Draft.ToString() ? Bank_Draft
                    : d.ToString() == PaymentMethodEnum.Direct_transfer_Postal_payment_slip.ToString() ? Direct_transfer_Postal_payment_slip
                    : d.ToString() == PaymentMethodEnum.Multiple.ToString() ? Multiple
                    : Other
                    ,
                    Selected = (d.ToString() == PaymentMethodEnum.Cash.ToString() ? Cash
                    : d.ToString() == PaymentMethodEnum.Cheque.ToString() ? Cheque
                    : d.ToString() == PaymentMethodEnum.Mobile_Financial_Service.ToString() ? Mobile_Financial_Service
                    : d.ToString() == PaymentMethodEnum.Bank_Draft.ToString() ? Bank_Draft
                    : d.ToString() == PaymentMethodEnum.Direct_transfer_Postal_payment_slip.ToString() ? Direct_transfer_Postal_payment_slip
                    : d.ToString() == PaymentMethodEnum.Multiple.ToString() ? Multiple
                    : Other
                    ) == value ? true : false
                }).ToList());
            return list;
        }
    }
}
