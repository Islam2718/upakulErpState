using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Command
{
    public class UpdateGroupWiseEmployeeAssignCommand : IRequest<CommadResponse>
    {
        public int Id { get; set; }
        public string? ReleaseNote { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
    }
}
