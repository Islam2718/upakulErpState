using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Handler
{
   public class UpdateMemberMigrateCommandHandler: IRequestHandler<UpdateMemberMigrateCommand, CommadResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _repository;

        public UpdateMemberMigrateCommandHandler(IMapper mapper, IMemberRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateMemberMigrateCommand request, CancellationToken cancellationToken)
        {
            var member = _repository.GetById(request.MemberId);
            if (member == null)
                return new CommadResponse("Member not found", HttpStatusCode.NotFound);

            member.IsMigrated = request.IsMigrated;
            member.MigratedNote = request.MigratedNote;
            member.MigrateDate = DateTime.Now;
            member.MigrateBy = request.MigrateBy;

            bool isSuccess = await _repository.UpdateAsync(member);
            return isSuccess
                ? new CommadResponse(MessageTexts.migrate_success, HttpStatusCode.Accepted)
                : new CommadResponse(MessageTexts.migrate_failed, HttpStatusCode.BadRequest);
        }
    }
}
