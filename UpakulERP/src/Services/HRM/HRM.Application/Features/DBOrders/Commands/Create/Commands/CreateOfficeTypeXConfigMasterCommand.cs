using HRM.Domain.ViewModels;
using MediatR;
using System.Collections.Generic;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateOfficeTypeXConfigMasterCommand : IRequest<CommadResponse>
    {
        public int OfficeTypeId { get; set; }
        public int ApplicantDesignationId { get; set; }
        public string LeaveCategoryId { get; set; }
        public List<CreateOfficeTypeXConfigureDetailsCommand> Mappings { get; set; } = new();
    
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
      
    }
}
