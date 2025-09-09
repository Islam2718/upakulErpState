using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;


namespace MF.Application.Features.DBOrders.Queries.Component
{
   public class ComponentByIdQueryHandler : IRequestHandler<ComponentByIdQuery, Domain.Models.Component>
    {
        IComponentRepository _repository;
        IMapper _mapper;
        public ComponentByIdQueryHandler(IComponentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Domain.Models.Component> Handle(ComponentByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return obj;//_mapper.Map<ComponentVM>(obj);
        }
    }
}
