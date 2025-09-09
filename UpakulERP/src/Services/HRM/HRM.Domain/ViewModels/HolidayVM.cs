namespace HRM.Domain.ViewModels
{
    public class HolidayVM
    {
        public int HolidayId { get; set; }
        public string? HolidayType { get; set; }
        public string HolidayName { get; set; }
        public int? DateNumber { get; set; }
        public int? MonthNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
