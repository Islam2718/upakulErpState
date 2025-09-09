using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Queries.BoardUniversity;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.Training
{
    class GetDataByIdHandler : IRequestHandler<GetDataByIdQuery, TrainingVM>
    {
        ITrainingRepository _repository;
        IMapper _mapper;
        public GetDataByIdHandler(ITrainingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<TrainingVM> Handle(GetDataByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return _mapper.Map<TrainingVM>(obj);
        }

    }
}
