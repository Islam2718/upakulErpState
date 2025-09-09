using System.Net;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Domain.Models.Loan;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Handler
{
    public class UpdatePurposeCommandHandler : IRequestHandler<UpdatePurposeCommand, CommadResponse>
    {
        IMapper _mapper;
        IPurposeRepository _repository;
        public UpdatePurposeCommandHandler(IMapper mapper, IPurposeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(UpdatePurposeCommand request, CancellationToken cancellationToken)
        {
            var Obj = _repository.GetById(request.Id);
            var _obj = _mapper.Map<UpdatePurposeCommand, Purpose>(request, Obj);
            bool isSuccess = await _repository.UpdateAsync(_obj);
            return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
        }
    }
}
