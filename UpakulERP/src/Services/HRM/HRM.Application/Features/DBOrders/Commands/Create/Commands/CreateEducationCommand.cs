using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateEducationCommand : IRequest<CommadResponse>
    {
        //public int EducationId { get; set; }
        public string EducationName { get; set; }
        public string EducationDescription { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }
}
