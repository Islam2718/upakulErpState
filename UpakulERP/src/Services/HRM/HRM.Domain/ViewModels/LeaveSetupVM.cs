using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.ViewModels
{
   public class LeaveSetupVM
    {


            public int LeaveTypeId { get; set; }
            public string LeaveCategoryId { get; set; }
            public string LeaveTypeName { get; set; }
            public string EmployeeTypeId { get; set; }
            public int YearlyMaxLeave { get; set; }
            public int YearlyMaxAvailDays { get; set; }
            public string LeaveGender { get; set; }
            public DateTime EffectiveStartDate { get; set; }
            public DateTime? EffectiveEndDate { get; set; }


     }

}

