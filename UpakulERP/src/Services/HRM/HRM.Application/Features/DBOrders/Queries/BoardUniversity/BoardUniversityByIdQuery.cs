using HRM.Domain.ViewModels;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.BoardUniversity
{
    public class BoardUniversityByIdQuery : IRequest<BoardUniversityVM>
    {
        public int id { get; set; }
        public BoardUniversityByIdQuery(int id)
        {
            this.id = id;
        }
    }
}
