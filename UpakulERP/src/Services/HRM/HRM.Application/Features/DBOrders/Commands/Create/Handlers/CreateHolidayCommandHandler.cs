using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using HRM.Domain.Models;
using MediatR;
using System.Net;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateHoliDayCommandHandler :  IRequestHandler<CreateHoliDayCommand, CommadResponse>
   {
        IMapper _mapper;
        IHoliDayRepository _holidayRepository;
    public CreateHoliDayCommandHandler(IMapper mapper, IHoliDayRepository holidayRepository)
    {
        _mapper = mapper;
        _holidayRepository = holidayRepository;
    }

    public async Task<CommadResponse> Handle(CreateHoliDayCommand request, CancellationToken cancellationToken)
    {
        var obj = _mapper.Map<HoliDay>(request);
        bool isSuccess = await _holidayRepository.AddAsync(obj);
        return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created,ReturnId:obj.HolidayId) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
    }
}
}