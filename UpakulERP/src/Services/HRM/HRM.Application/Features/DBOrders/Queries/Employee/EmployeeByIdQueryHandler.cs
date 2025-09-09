using AutoMapper;
using HRM.Application.Contacts.Persistence;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.Employee
{
    public class EmployeeByIdQueryHandler : IRequestHandler<EmployeeByIdQuery, HRM.Domain.Models.Employee>
    {
        IEmployeeRepository _repository;
        IMapper _mapper;
        public EmployeeByIdQueryHandler(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<HRM.Domain.Models.Employee> Handle(EmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = await _repository.GetById(request.id);
            return obj;
        }
    }
}
