using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Domain.ViewModels
{
    public class DailyProcessVM
    {
        public int Id { get; set; }
        public int? OfficeId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Boolean IsDayClose { get; set; }
        public DateTime? ReOpenDate { get; set; }

    }

}
