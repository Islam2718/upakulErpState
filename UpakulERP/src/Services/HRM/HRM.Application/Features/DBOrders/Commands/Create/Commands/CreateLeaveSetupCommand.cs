using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Commands
{
   public class CreateLeaveSetupCommand : IRequest<CommadResponse>
    {
            public string LeaveCategoryId { get; set; }
            public string LeaveTypeName { get; set; }
            public string? EmployeeTypeId { get; set; }
            public int YearlyMaxLeave { get; set; }
            public int YearlyMaxAvailDays { get; set; }
            public string? LeaveGender { get; set; }
            public string EffectiveStartDate { get; set; }
            public string? EffectiveEndDate { get; set; }

            // Common Audit Fields
            public int? CreatedBy { get; set; }
            public DateTime? CreatedOn { get; set; } = DateTime.Now;
  
        }

    }

