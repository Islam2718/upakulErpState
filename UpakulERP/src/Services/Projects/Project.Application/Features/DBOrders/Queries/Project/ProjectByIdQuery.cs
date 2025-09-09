using MediatR;
using Project.Domain.ViewModels;

namespace Project.Application.Features.DBOrders.Queries.Project
{
    public class ProjectByIdQuery : IRequest<ProjectVM>
    {
        public int id { get; set; }
        public ProjectByIdQuery(int id)
        {
            this.id = id;
        }
    }

}
