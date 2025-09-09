using MediatR;
using Utility.Domain;

namespace HRM.Application.Features.DBOrders.Queries.Designation
{
    public class DesignationDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
    }
}
