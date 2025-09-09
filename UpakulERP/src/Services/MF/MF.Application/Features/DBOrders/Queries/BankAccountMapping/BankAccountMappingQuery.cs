using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.BankAccountMapping
{
    public class BankAccountMappingQuery : IRequest<List<CustomSelectListItem>>
    {
        public int id { get; set; }
        public BankAccountMappingQuery(int? id)
        {
            this.id = id ?? 0;
        }
    }



}
