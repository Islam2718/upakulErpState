using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Domain;

namespace Project.Application.Features.DBOrders.Queries.Country
{
    public class CountryDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public int id { get; set; }
        public CountryDropdownQuery(int id)
        {
            this.id = id;
        }
    }
}
