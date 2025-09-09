using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.ViewModels
{
  public  class OfficeTypeXConfigureDetailsVM
    {
        public int ApproverDesignationId { get; set; }
        public int LevelNo { get; set; }
        public int? MinimumLeave { get; set; }
        public int? MaximumLeave { get; set; }



    }
}
