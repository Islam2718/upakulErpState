using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MF.Application.Contacts.Persistence;
using MediatR;
using MF.Domain.Models.View;
namespace MF.Application.Features.DBOrders.Queries.GraceSchedule
{
    public class GraceScheduleByIdHandler : IRequestHandler<GraceScheduleByIdQuery, Domain.Models.GraceSchedule>
    {
        IGraceScheduleRepository _repository;
        IMapper _mapper;
    public GraceScheduleByIdHandler(IGraceScheduleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Domain.Models.GraceSchedule> Handle(GraceScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetById(request.id);
    }
}

}

