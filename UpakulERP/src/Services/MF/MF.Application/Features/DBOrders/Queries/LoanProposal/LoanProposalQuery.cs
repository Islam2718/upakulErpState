using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.LoanProposal
{
    public class LoanProposalQuery : IRequest<List<CustomSelectListItem>>
    {
        public int id { get; set; }
        public LoanProposalQuery(int? id)
        {
            this.id = id ?? 0;
        }
    }

}
