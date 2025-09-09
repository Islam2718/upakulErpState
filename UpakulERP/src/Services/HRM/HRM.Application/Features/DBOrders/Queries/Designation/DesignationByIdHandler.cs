using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.ViewModels;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.Designation
{

    public class DesignationByIdHandler : IRequestHandler<DesignationByIdQuery, DesignationVM>
    {
        IDesignationRepository _repository;
        IMapper _mapper;
        public DesignationByIdHandler(IDesignationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DesignationVM> Handle(DesignationByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return _mapper.Map<DesignationVM>(obj);
        }
    }

}
