using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Domain.Models.Views;
using Global.Domain.ViewModels;
using MediatR;

namespace Global.Application.Features.DBOrders.Queries.GeoLocation
{
    public class GetOfficeByIdHandler : IRequestHandler<OfficeByIdQuery, VWOffice>
    {
        IOfficeRepository _repository;
        IMapper _mapper;
        public GetOfficeByIdHandler(IOfficeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<VWOffice> Handle(OfficeByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return _mapper.Map<VWOffice>(obj);
        }
    }
}
