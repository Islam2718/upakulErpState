using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Application.Features.DBOrders.Queries.GeoLocation;
using Global.Domain.Models.Views;
using Global.Domain.ViewModels;
using MediatR;

namespace Global.Application.Features.DBOrders.Queries.Office
{
    public class OfficeByIdHandler : IRequestHandler<OfficeByIdQuery, VWOffice>
    {
        IOfficeRepository _repository;
        IMapper _mapper;
        public OfficeByIdHandler(IOfficeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<VWOffice> Handle(OfficeByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetByIdFromView(request.id);
            return _mapper.Map<VWOffice>(obj);
        }
    }
}
