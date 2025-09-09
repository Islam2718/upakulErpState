using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.Group
{
    class GetSamityByIdHandler : IRequestHandler<GroupByIdQuery, SamityVM>
    {
        IGroupRepository _repository;
        IMapper _mapper;
        public GetSamityByIdHandler(IGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<SamityVM> Handle(GroupByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return _mapper.Map<SamityVM>(obj);
        }
    }
}
