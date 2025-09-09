using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;
using HRM.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Update.Handlers
{
   public class UpdateHoliDayCommandHandler : IRequestHandler<UpdateHoliDayCommand, CommadResponse>
    {
        IMapper _mapper;
        IHoliDayRepository _repository;
        public UpdateHoliDayCommandHandler(IMapper mapper, IHoliDayRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateHoliDayCommand request, CancellationToken cancellationToken)
        {
            var varObj = _repository.GetById(request.HoliDayId);
            var obj = _mapper.Map<UpdateHoliDayCommand, HoliDay>(request, varObj);
            
            bool isSuccess = await _repository.UpdateAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
        }
    }

}