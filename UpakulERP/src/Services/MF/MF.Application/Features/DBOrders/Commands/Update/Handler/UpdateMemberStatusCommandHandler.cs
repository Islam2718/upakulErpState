using System.Net;
using MediatR;
using MF.Application.Contacts.Constants;
using MF.Application.Contacts.Enums;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Domain.Models.Saving;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Handler
{
    public class UpdateMemberStatusCommandHandler : IRequestHandler<UpdateMemberStatusCommand, CommadResponse>
    {
        IMemberRepository _repository;
        IComponentRepository _componentRepository;
        IGeneralSavingRepository _generalSavingRepository;
        public UpdateMemberStatusCommandHandler(IMemberRepository repository, IComponentRepository componentRepository, IGeneralSavingRepository generalSavingRepository)
        {
            _repository = repository;
            _componentRepository = componentRepository;
            _generalSavingRepository = generalSavingRepository;
        }

        public async Task<CommadResponse> Handle(UpdateMemberStatusCommand request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.MemberId);
            obj.MemberStatus = request.IsApproved ? MemberStatus.ApprovedMember : MemberStatus.RejectedMember;
            if (request.IsApproved)
            {
                obj.ApprovedBy = request.ApprovedBy;
                obj.ApprovedDate = DateTime.Now;
                obj.ApprovedNote = request.Note;
                var com_lst = _componentRepository.GetMany(x => x.ComponentType == Component.Security_Saving || x.ComponentType == Component.Volenter_Saving);
                if (com_lst.Any(x => x.ComponentType == Component.Security_Saving))
                {
                    var _obj = com_lst.FirstOrDefault(x => x.ComponentType == Component.Security_Saving);
                    GeneralSavingSummary summary = new GeneralSavingSummary()
                    {
                        OfficeId = obj.OfficeId,
                        MemberId = obj.MemberId,
                        GroupId = obj.GroupId,
                        ComponentId = _obj.Id,
                        InterestRate = _obj.InterestRate
                    };
                   await _generalSavingRepository.NewSavings(summary);
                }
                if (com_lst.Any(x => x.ComponentType == Component.Volenter_Saving))
                {
                    var _obj = com_lst.FirstOrDefault(x => x.ComponentType == Component.Volenter_Saving);
                    GeneralSavingSummary summary = new GeneralSavingSummary()
                    {
                        OfficeId = obj.OfficeId,
                        MemberId = obj.MemberId,
                        GroupId = obj.GroupId,
                        ComponentId = _obj.Id,
                        InterestRate = _obj.InterestRate
                    };
                   await _generalSavingRepository.NewSavings(summary);
                }
            }
            else
            {
                obj.RejectBy = request.RejectBy;
                obj.RejectDate = DateTime.Now;
                obj.RejectReason = request.Note;
            }
            bool isSuccess = await _repository.UpdateAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
        }
    }
}
