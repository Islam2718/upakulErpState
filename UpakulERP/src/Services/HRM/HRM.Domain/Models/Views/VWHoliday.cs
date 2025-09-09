using System.ComponentModel.DataAnnotations.Schema;

namespace HRM.Domain.Models.Views
{
    [Table("vw_Holiday", Schema = "dbo")]
    public class VWHoliday
    {
        public int HolidayId { get; set; }
        public string? HolidayType { get; set; }
        public string HolidayName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
