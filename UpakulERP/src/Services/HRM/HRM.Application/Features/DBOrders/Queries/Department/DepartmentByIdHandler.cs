using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.ViewModels;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.Department
{
   public class DepartmentByIdHandler : IRequestHandler<DepartmentByIdQuery, DepartmentVM>
    {
        IDepartmentRepository _repository;
        IMapper _mapper;
        public DepartmentByIdHandler(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<DepartmentVM> Handle(DepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return _mapper.Map<DepartmentVM>(obj);
        }
    }

}
