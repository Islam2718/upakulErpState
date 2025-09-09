using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace MF.Domain.Models
{

    [Table("DailyProcess", Schema = "pros")]
    public class DailyProcess : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Boolean IsDayClose { get; set; } = false;
        public DateTime? ReOpenDate { get; set; }
    }
}
