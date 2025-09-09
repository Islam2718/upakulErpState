using MediatR;
using Utility.Domain;

namespace HRM.Application.Features.DBOrders.Queries.Education
{
    public class EducationDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
    }
}
