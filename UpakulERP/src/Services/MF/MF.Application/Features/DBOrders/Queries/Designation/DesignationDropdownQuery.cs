using MediatR;
using Utility.Domain;
using Utility.Enums;

namespace MF.Application.Features.DBOrders.Queries.Designation
{
    public class DesignationDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        //public int officeTypeId { get; set; }
        public int officeId { get; set; }
        public DesignationDropdownQuery(int officeId/*, int officeTypeId*/)
        {
            //this.officeTypeId = officeTypeId;
            this.officeId =officeId ;
        }
    }
}
