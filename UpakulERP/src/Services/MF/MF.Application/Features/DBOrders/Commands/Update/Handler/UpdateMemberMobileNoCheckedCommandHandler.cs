using System.Net;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Domain.Models;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Handler
{
    public class UpdateMemberMobileNoCheckedCommandHandler : IRequestHandler<UpdateMemberMobileNoCheckedCommand, CommadResponse>
    {
        IMapper _mapper;
        IMemberRepository _repository;
        public UpdateMemberMobileNoCheckedCommandHandler(IMapper mapper, IMemberRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdateMemberMobileNoCheckedCommand request, CancellationToken cancellationToken)
        {
            var member = _repository.GetById(request.MemberId);
            if (member == null)
                return new CommadResponse("Member not found", HttpStatusCode.NotFound);
            member.OTPNo = request.OTPNo;
            member.IsCheckedInContactNo = request.IsChecked;
            member.CheckedBy = request.CheckedBy;
            member.CheckedDate = request.CheckedDate;

            bool isSuccess = await _repository.UpdateAsync(member);
            return isSuccess
                ? new CommadResponse(MessageTexts.checked_success, HttpStatusCode.Accepted)
                : new CommadResponse(MessageTexts.checked_failed, HttpStatusCode.BadRequest);
        }

    }
}
