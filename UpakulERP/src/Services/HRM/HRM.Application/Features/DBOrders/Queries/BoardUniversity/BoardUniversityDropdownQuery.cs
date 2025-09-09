using MediatR;
using Utility.Domain;

namespace HRM.Application.Features.DBOrders.Queries.BoardUniversity
{
    public class BoardUniversityDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
    }
}
