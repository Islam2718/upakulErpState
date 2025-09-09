using HRM.Domain.ViewModels;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.Education
{
    public class GetEducationByIdQuery : IRequest<EducationVM>
    {
        public int id { get; set; }
        public GetEducationByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
