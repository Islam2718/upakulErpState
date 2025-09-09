using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Features.DBOrders.Queries.LeaveMapping
{
   public class GetMasterByIdHandler: IRequestHandler<GetMasterByIdQuery, OfficeTypeXConfigMasterVM>
    {
         IOfficeTypeXConfigMasterRepository _repository;
         IMapper _mapper;

        public GetMasterByIdHandler(IOfficeTypeXConfigMasterRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OfficeTypeXConfigMasterVM> Handle(GetMasterByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.Id);
            return _mapper.Map<OfficeTypeXConfigMasterVM>(obj);
        }
    }
}

