using HRM.Application.Contacts.Persistence;
using MediatR;
using Utility.Constants;
using Utility.Domain;

namespace HRM.Application.Features.DBOrders.Queries.Department
{
    public class EmployeeTypeDropdownQueryHandler : IRequestHandler<DepartmentDropdownQuery, List<CustomSelectListItem>>
    {
        IDepartmentRepository _repository;
        public EmployeeTypeDropdownQueryHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<CustomSelectListItem>> Handle(DepartmentDropdownQuery request, CancellationToken cancellationToken)
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = true });
            list.AddRange(_repository.GetAll().ToList().Select(s => new CustomSelectListItem
            {
                Text = (!string.IsNullOrWhiteSpace(s.DepartmentCode) ? $"({s.DepartmentCode}) " : "") + s.DepartmentName,
                Value = s.DepartmentId.ToString()
            }));
            return list;
        }

    }
}
