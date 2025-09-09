using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.ViewModels;
using MediatR;
namespace HRM.Application.Features.DBOrders.Queries.HoliDay
{
    public class HoliDayByIdHandler : IRequestHandler<HolidayByIdQuery, HolidayVM>
    {
        IHoliDayRepository _repository;
        IMapper _mapper;
    public HoliDayByIdHandler(IHoliDayRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<HolidayVM> Handle(HolidayByIdQuery request, CancellationToken cancellationToken)
    {
        var obj = _repository.GetById(request.id);
        return _mapper.Map<HolidayVM>(obj);
    }
}

}

