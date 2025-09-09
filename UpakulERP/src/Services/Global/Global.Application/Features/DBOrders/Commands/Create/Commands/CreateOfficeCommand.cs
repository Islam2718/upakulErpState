using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateOfficeCommand : IRequest<CommadResponse>
    {
        public int OfficeType { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string? OfficeShortCode { get; set; }
        public string? OfficeAddress { get; set; }
        public string? OperationStartDate { get; set; }
       // public DateTime? OperationEndDate { get; set; }
        public string? OfficeEmail { get; set; }
        public string? OfficePhoneNo { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public int? ParentId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
