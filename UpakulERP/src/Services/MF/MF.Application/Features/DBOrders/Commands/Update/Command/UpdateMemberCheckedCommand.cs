using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Command
{
   public class UpdateCheckedforOTPVerifyCommand : IRequest<CommadResponse>
    {
        public int MemberId { get; set; }
        public bool IsCheckedInContactNo { get; set; } = true;
        public int? CheckedBy { get; set; }               
        public DateTime? CheckedDate { get; set; } = DateTime.Now;
        public int? RejectBy { get; set; }          
        public DateTime? RejectDate { get; set; }
    }
}
