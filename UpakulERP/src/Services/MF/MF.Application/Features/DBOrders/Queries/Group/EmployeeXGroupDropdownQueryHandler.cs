using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.Group
{
    public class EmployeeXGroupDropdownQueryHandler : IRequestHandler<EmployeeXGroupDropdownQuery, List<CustomSelectListItem>>
    {
        IGroupRepository _repository;
        IMapper _mapper;

        public EmployeeXGroupDropdownQueryHandler(IGroupRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CustomSelectListItem>> Handle(EmployeeXGroupDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj = _repository.GetGroupByEmployeeIdDropdown(request.empId);
            return lstObj;
        }
    }
}
