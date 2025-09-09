using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.Models.Views;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.Employee
{
    public class EmployeeByIdViewQueryHandler : IRequestHandler<EmployeeByIdViewQuery, VWEmployee>
    {
        IEmployeeRepository _repository;
        IMapper _mapper;
        public EmployeeByIdViewQueryHandler(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<VWEmployee> Handle(EmployeeByIdViewQuery request, CancellationToken cancellationToken)
        {
            var obj =await _repository.GetById_View(request.id);
            return _mapper.Map<VWEmployee>(obj);
        }
    }
}
