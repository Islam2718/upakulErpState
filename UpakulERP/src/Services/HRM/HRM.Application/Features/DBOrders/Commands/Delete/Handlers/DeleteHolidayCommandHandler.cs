using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Delete.Commands;
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

namespace HRM.Application.Features.DBOrders.Commands.Delete.Handlers
{
    public class DeleteHoliDayCommandHandler : IRequestHandler<DeleteHoliDayCommand, CommadResponse>
    {
        IMapper _mapper;
        IHoliDayRepository _repository;
        public DeleteHoliDayCommandHandler(IMapper mapper, IHoliDayRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CommadResponse> Handle(DeleteHoliDayCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.HoliDayId);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeleteHoliDayCommand, HoliDay>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }

}
