using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models.View;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.MainPurpose
{
    public class GetPurposeByIdQueryHandler : IRequestHandler<GetPurposeByIdQuery, VwPurpose>
    {
        IPurposeRepository _repository;
        IMapper _mapper;
        public GetPurposeByIdQueryHandler(IPurposeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<VwPurpose> Handle(GetPurposeByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetByIdXView(request.id);
            return obj;
        }
    }
}
