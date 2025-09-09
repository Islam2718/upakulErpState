using System.ComponentModel.DataAnnotations;

namespace MF.Application.Contacts.Enums
{
    public class LoanApproveStatus
    {
        public const string Pending = "P";
        public const string Approved = "A";
        public const string Reject = "J";
        public const string Revised = "V";

        public const string ReadyForDisbursed = "R";
        public const string Disbursed = "D";

        public enum LoanApproveStatusEnum
        {
            [Display(Name = "Pending")]
            Pending,
            [Display(Name = "Approved")]
            Approved,
            [Display(Name = "Reject")]
            Reject,
            [Display(Name = "Revised")]
            Revised,
            [Display(Name = "Ready For Disbursed")]
            ReadyForDisbursed,
            [Display(Name = "Disbursed")]
            Disbursed,
        }
    }
}