using MediatR;
using Utility.Domain;

namespace HRM.Application.Features.DBOrders.Queries.Department
{
    public class DepartmentDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
    }
}
