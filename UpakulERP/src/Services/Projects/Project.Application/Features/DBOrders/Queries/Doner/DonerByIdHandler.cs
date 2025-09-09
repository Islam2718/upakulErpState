using AutoMapper;
using MediatR;
using Project.Application.Contacts.Persistence;
using roject.Domain.ViewModels;

namespace Project.Application.Features.DBOrders.Queries.Doner
{
    public class DonerByIdHandler : IRequestHandler<DonerByIdQuery, DonerVM>
    {
        IDonerRepository _repository;
        IMapper _mapper;
        public DonerByIdHandler(IDonerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DonerVM> Handle(DonerByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return _mapper.Map<DonerVM>(obj);
        }
    }
}
