using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Update.Commands
{
    public class UpdateEducationCommand : IRequest<CommadResponse>
    {
        public int EducationId { get; set; }
        public string EducationName { get; set; }
        public string EducationDescription { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }=DateTime.Now;
    }


}
