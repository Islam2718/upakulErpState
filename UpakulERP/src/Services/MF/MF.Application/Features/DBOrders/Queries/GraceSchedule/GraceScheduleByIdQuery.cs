using MediatR;
using MF.Domain.Models.View;

namespace MF.Application.Features.DBOrders.Queries.GraceSchedule
{
    public class GraceScheduleByIdQuery : IRequest<Domain.Models.GraceSchedule>
    {
        public int id { get; set; }
        public GraceScheduleByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
