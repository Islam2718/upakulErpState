using System.ComponentModel.DataAnnotations;
using Utility.Constants;
using Utility.Domain;
using Utility.Extensions;

namespace MF.Application.Contacts.Enums
{
    public class PaymentType
    {
        public const string Cash = "CASH";
        public const string Bearer_Cheque = "CHQ";
        public const string Account_Pay_Cheque = "APC";
        public const string Mobile_Financial_Service = "MFS";
        public const string BFTN = "BFTN";
        public const string RTGS = "RTGS";
        public const string NPBS = "NPBS";


        public enum PaymentTypeEnum
        {
            [Display(Name = "Cash")]
            Cash,
            [Display(Name = "Bearer Cheque")]
            Bearer_Cheque,
            [Display(Name = "Account Pay Cheque")]
            Account_Pay_Cheque,
            [Display(Name = "Mobile Financial Service")]
            Mobile_Financial_Service,
            [Display(Name = "BFTN")]
            BFTN,
            [Display(Name = "RTGS")]
            RTGS,
            [Display(Name = "NPBS")]
            NPBS,
        }
        public List<CustomSelectListItem> GetPaymentTypeDropDown(string value = "")
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = string.IsNullOrEmpty(value) ? true : false });
            list.AddRange(Enum.GetValues(typeof(PaymentTypeEnum))
                .Cast<PaymentTypeEnum>()
                .Select(d => new CustomSelectListItem
                {
                    Text = d.GetEnumDisplayName(),
                    Value = d.ToString() == PaymentTypeEnum.Cash.ToString() ? Cash
                    : d.ToString() == PaymentTypeEnum.Bearer_Cheque.ToString() ? Bearer_Cheque
                    : d.ToString() == PaymentTypeEnum.Mobile_Financial_Service.ToString() ? Mobile_Financial_Service
                    : d.ToString() == PaymentTypeEnum.Account_Pay_Cheque.ToString() ? Account_Pay_Cheque
                    : d.ToString() == PaymentTypeEnum.BFTN.ToString() ? BFTN
                    : d.ToString() == PaymentTypeEnum.RTGS.ToString() ? RTGS
                    : d.ToString() == PaymentTypeEnum.NPBS.ToString() ? NPBS
                    : ""
                    ,
                    Selected = false
                }).ToList());
            return list;
        }
    }
}
