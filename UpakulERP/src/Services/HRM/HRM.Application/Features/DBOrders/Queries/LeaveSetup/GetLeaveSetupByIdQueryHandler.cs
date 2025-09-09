using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.ViewModels;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.LeaveSetup
{
    public class GetLeaveSetupByIdHandler : IRequestHandler<LeaveSetupByIdQuery, LeaveSetupVM>
    {
        ILeaveSetupRepository _repository;
        IMapper _mapper;
    public GetLeaveSetupByIdHandler(ILeaveSetupRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<LeaveSetupVM> Handle(LeaveSetupByIdQuery request, CancellationToken cancellationToken)
    {
        var obj = _repository.GetById(request.id);
        return _mapper.Map<LeaveSetupVM>(obj);
    }
}

}

