using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.Member
{
    public class MemberByIdQuery : IRequest<MemberVM>
    {
        public int id { get; set; }
        public MemberByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
