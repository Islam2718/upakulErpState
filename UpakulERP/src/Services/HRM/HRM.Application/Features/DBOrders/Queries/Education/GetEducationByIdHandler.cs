using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.ViewModels;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.Education
{
    class GetEducationByIdHandler : IRequestHandler<GetEducationByIdQuery, EducationVM>
    {
        IEducationRepository _repository;
        IMapper _mapper;
        public GetEducationByIdHandler(IEducationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<EducationVM> Handle(GetEducationByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return _mapper.Map<EducationVM>(obj);
        }
    }

}
