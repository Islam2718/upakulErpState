using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Queries.CommonIdGenerator;
using MF.Domain.Models;

namespace MF.Application.Features.DBOrders.Queries.CodeGenerator
{
    public class IdGenerateQueryHandler : IRequestHandler<IdGenerateQuery, List<IdGenerate>>
    {
        IIdGeneratorRepository _repository;
        IMapper _mapper;
        public IdGenerateQueryHandler(IIdGeneratorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<IdGenerate>> Handle(IdGenerateQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetAll();
        }

    }
}
