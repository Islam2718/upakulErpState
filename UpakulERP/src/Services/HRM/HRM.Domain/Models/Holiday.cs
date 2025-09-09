using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Utility.Domain;

namespace HRM.Domain.Models
{
    [Table("Holiday", Schema = "dbo")]
    public class HoliDay : EntityBase
    {
        [Key]
        public int HolidayId { get; set; }
        public string? HolidayType { get; set; } = "S";
        public string HolidayName { get; set; }
        public int? DateNumber { get; set; }
        public int? MonthNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


    }
}

