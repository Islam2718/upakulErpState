using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.MasterComponent
{
  public  class MasterComponentDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public int? Id { get; set; }
        public MasterComponentDropdownQuery(int? pId)
        {
            Id = pId;
        }
    }
}