using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Domain;

namespace Project.Application.Features.DBOrders.Queries.Doner
{
    public class DonerDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public DonerDropdownQuery()
        {
        }
    }
}
