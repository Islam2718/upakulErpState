namespace MF.Domain.Models.Functions
{
    public class RepaymentSchedule
    {
        public int ScheduleNo { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string? DayName { get; set; }
        public double? ServiceCharge { get; set; }
        public double? PrincipalRepayment { get; set; }
        public double? Installment { get; set; }
        public double? BeginningBalance { get; set; }
        public double? RemainingBalance { get; set; }
    }
}
