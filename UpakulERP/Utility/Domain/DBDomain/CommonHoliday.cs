using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Utility.Domain.DBDomain
{
    [Table("Holiday", Schema = "dbo")]
    public class CommonHoliday
    {
        [Key]
        public int HolidayId { get; set; }
        public string? HolidayType { get; set; } = "S";
        public string HolidayName { get; set; }
        public int? DateNumber { get; set; }
        public int? MonthNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
