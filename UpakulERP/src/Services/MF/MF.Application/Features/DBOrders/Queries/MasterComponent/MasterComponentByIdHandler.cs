using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Queries.MasterComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Application.Features.DBOrders.Queries.MasterComponent
{
  public  class MasterComponentByIdHandler : IRequestHandler<MasterComponentByIdQuery, MasterComponentVM>
    {
        IMasterComponentRepository _repository;
        IMapper _mapper;
        public MasterComponentByIdHandler(IMasterComponentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MasterComponentVM> Handle(MasterComponentByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return _mapper.Map<MasterComponentVM>(obj);
        }
    }
}