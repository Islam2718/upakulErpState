using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Domain.ViewModels
{
    public class LoanApprovalVM
    {
        public int Layer { get; set; }
        public string ApprovalType { get; set; }
        public int DesignationId { get; set; }
        public string Method { get; set; }
        public int Value { get; set; }
    }
}
