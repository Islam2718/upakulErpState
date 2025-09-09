using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Delete.Handler
{
      public class DeleteGraceScheduleHandler : IRequestHandler<DeleteGraceScheduleCommand, CommadResponse>
    {
        IMapper _mapper;
        IGraceScheduleRepository _repository;

        public DeleteGraceScheduleHandler(IMapper mapper, IGraceScheduleRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(DeleteGraceScheduleCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.Id);
            if (_obj != null)
            {
                // Corrected mapping: MappingGs to MappingGs instead of DeleteMappingCommand to MappingGs
                var obj = _mapper.Map<DeleteGraceScheduleCommand,GraceSchedule>(request, _obj); // map into existing entity
                bool isSuccess = await _repository.UpdateAsync(obj);
                return isSuccess
                    ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted)
                    : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest);
            }
            else
            {
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
            }
        }
    }
}
