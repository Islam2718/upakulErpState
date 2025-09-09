using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace Project.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateProjectCommand : IRequest<CommadResponse>
    {
        public int DonerId { get; set; }
        public string ProjectShortName { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectType { get; set; }
        public string? Objective { get; set; }
        public int ChipEmployeeId { get; set; }
        public int TotalStaff { get; set; }
        public string ProjectStartDate { get; set; }
        public string ProjectEndDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
