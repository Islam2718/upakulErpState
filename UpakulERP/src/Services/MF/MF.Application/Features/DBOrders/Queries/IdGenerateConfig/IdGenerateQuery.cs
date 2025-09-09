using MediatR;
using MF.Domain.Models;

namespace MF.Application.Features.DBOrders.Queries.CommonIdGenerator
{
    public class IdGenerateQuery : IRequest<List<IdGenerate>>
    {
        public IdGenerateQuery()
        {

        }
    }
}
