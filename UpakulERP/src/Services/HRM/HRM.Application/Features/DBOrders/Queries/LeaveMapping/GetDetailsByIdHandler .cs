using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Queries.HoliDay;
using HRM.Domain.ViewModels;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.LeaveMapping
{
    public class GetDetailsByIdHandler : IRequestHandler<GetDetailsByIdQuery, OfficeTypeXConfigureDetailsVM>
    {
        IOfficeTypeXConfigureDetailsRepository _repository;
        IMapper _mapper;

        public GetDetailsByIdHandler(IOfficeTypeXConfigureDetailsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OfficeTypeXConfigureDetailsVM> Handle(GetDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.Id);
            return _mapper.Map<OfficeTypeXConfigureDetailsVM>(obj);
        }
    }
}
