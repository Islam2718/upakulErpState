using MediatR;
using Project.Application.Contacts.Persistence;
using Utility.Constants;
using Utility.Domain;

namespace Project.Application.Features.DBOrders.Queries.Project
{
    public class ProjectDropdownQueryHandler : IRequestHandler<ProjectDropdownQuery, List<CustomSelectListItem>>
    {
        IProjectRepository _repository;
        public ProjectDropdownQueryHandler(IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<CustomSelectListItem>> Handle(ProjectDropdownQuery request, CancellationToken cancellationToken)
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = true });
            list.AddRange(_repository.GetAll().ToList().Select(s => new CustomSelectListItem
            {
                Text = (!string.IsNullOrWhiteSpace(s.ProjectShortName) ? $"{s.ProjectShortName} - {s.ProjectTitle}" : s.ProjectTitle),
                Value = s.ProjectId.ToString()
            }));
            return list;
        }
    }
}
