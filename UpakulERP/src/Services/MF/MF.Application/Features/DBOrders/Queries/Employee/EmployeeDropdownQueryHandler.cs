using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using Utility.Constants;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.Employee
{
    public class EmployeeDropdownQueryHandler : IRequestHandler<EmployeeDropdownQuery, List<CustomSelectListItem>>
    {
        IEmployeeRepository _repository;
        IMapper _mapper;
        public EmployeeDropdownQueryHandler(IEmployeeRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(EmployeeDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj = _repository.GetMany(x=>x.OfficeId==request.officeId);
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (request.employeeId == 0 ? true : false) });
            if(lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Selected = ((s.EmployeeId == request.employeeId) ? true : false),
                    Text = s.EmployeeCode +" - "+ s.EmployeeFullName,
                    Value = s.EmployeeId.ToString()
                })); 
            }
            return list;
        }
    }
}
