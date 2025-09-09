using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.Occupation
{
    class GetOccupationByIdHandler : IRequestHandler<OccupationByIdQuery, OccupationVM>
    {
        IOccupationRepository _repository;
        IMapper _mapper;
        public GetOccupationByIdHandler(IOccupationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<OccupationVM> Handle(OccupationByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return _mapper.Map<OccupationVM>(obj);
        }
    }



}
