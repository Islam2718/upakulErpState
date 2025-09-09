using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Domain.ViewModels;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.BankAccountMapping
{
    public class OfficeXBankDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public int _officeId { get; set; }
        public OfficeXBankDropdownQuery(int officeId)
        {
            this._officeId = officeId;
        }
    }

}
