using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Domain.ViewModels
{
   public class GraceScheduleVM
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public int? OfficeId { get; set; }
        public int? GroupId { get; set; }
        public int? LoanId { get; set; }
        public DateTime GraceFrom { get; set; }
        public DateTime GraceTo { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}
