using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Domain.Models;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateDailyProcessHandler : IRequestHandler<InitialDayProcessCommand, CommadResponse>
    {
        IMapper _mapper;
        IDailyProcessRepository _repository;
        public CreateDailyProcessHandler(IMapper mapper, IDailyProcessRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(InitialDayProcessCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.TransactionDate >= request.TransactionDate && c.IsActive==true && c.OfficeId==request.OfficeId).Any())
                return new CommadResponse("Transaction date can't be equal or greater then last initial date.", HttpStatusCode.NotAcceptable);
            else if(_repository.GetMany(c=>c.IsActive && !c.IsDayClose && c.OfficeId==request.OfficeId).Any())
                return new CommadResponse("This day already processed", HttpStatusCode.NotAcceptable);
            else if (_repository.CheckHolidays(c => c.IsActive && request.TransactionDate >=  c.StartDate && request.TransactionDate <=c.EndDate).Any())
                return new CommadResponse("This day is not valid... It is an holiday", HttpStatusCode.NotAcceptable);
            else
            {
                var obj = _mapper.Map<DailyProcess>(request);
                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }
        }
    }


}
