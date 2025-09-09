using System.ComponentModel.DataAnnotations;

namespace MF.Application.Contacts.Enums
{
    public class LoanStatus
    {
        public const string Regular = "R";
        public const string Watchful = "W";
        public const string Sub_Standard = "SS";
        public const string Doubtful = "SS";
        public const string Bad_Loan = "BL";
        public const string Write_off = "WO";
        public enum LoanStatusEnum
        {
            [Display(Name = "Regular")]
            Regular,
            [Display(Name = "Watchful")]
            Watchful,
            [Display(Name = "Sub-Standard")]
            Sub_Standard,
            [Display(Name = "Doubtful")]
            Doubtful,
            [Display(Name = "Bad Loan")]
            Bad_Loan,
            [Display(Name = "Write off")]
            Write_off,
        }
    }
}
