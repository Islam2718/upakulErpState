using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.EmployeeRegister
{
    public class GrpWiseEmployeeDropdownQuery : IRequest<MultipleDropdownForGrpWiseEmployeeVM>
    {
        public int _officeId { get; set; }
        public int _officeTypeId { get; set; }
        public GrpWiseEmployeeDropdownQuery(int officeId, int officeTypeId)
        {
            this._officeId = officeId;
            this._officeTypeId = officeTypeId;
        }
    }


}
