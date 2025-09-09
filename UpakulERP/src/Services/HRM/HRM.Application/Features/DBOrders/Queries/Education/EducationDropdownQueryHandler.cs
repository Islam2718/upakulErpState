using HRM.Application.Contacts.Persistence;
using MediatR;
using Utility.Constants;
using Utility.Domain;

namespace HRM.Application.Features.DBOrders.Queries.Education
{
    public class EducationDropdownQueryHandler : IRequestHandler<EducationDropdownQuery, List<CustomSelectListItem>>
    {
        IEducationRepository _repository;
        public EducationDropdownQueryHandler(IEducationRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<CustomSelectListItem>> Handle(EducationDropdownQuery request, CancellationToken cancellationToken)
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = true });
            list.AddRange(_repository.GetAll().ToList().Select(s => new CustomSelectListItem
            {
                Text =  s.EducationName,
                Value = s.EducationId.ToString()
            }));
            return list;
        }

    }
}
