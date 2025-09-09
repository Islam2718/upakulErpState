using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.OfficeComponentMapping
{
    class GetDataByIdQueryHandler : IRequestHandler<GetDataByIdQuery, List<OfficeComponentMappingVM>>
    {
        IMapper _mapper;
        IOfficeComponentMappingRepository _repository;
        public GetDataByIdQueryHandler(IOfficeComponentMappingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<OfficeComponentMappingVM>> Handle(GetDataByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetAllByComponentId(request.id);  //.GetById(request.id);
            return _mapper.Map<List<OfficeComponentMappingVM>>(obj);
        }
    }

}
