using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.BankAccountMapping
{
    public class OfficeBankAssignDropdownDataQuery : IRequest<OfficeBankAssignDropdownVM>
    {
        public int _officeId { get; set; }
        public OfficeBankAssignDropdownDataQuery(int officeId)
        {
            this._officeId = officeId;
        }
    }

}
