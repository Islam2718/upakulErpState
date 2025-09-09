using System.Net;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Domain.Models.Loan;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Delete.Handler
{
    public class DeletePurposeCommandHandler : IRequestHandler<DeletePurposeCommand, CommadResponse>
    {
        IMapper _mapper;
        IPurposeRepository _repository;
        public DeletePurposeCommandHandler(IMapper mapper, IPurposeRepository rpository)
        {
            _mapper = mapper;
            _repository = rpository;
        }
        public async Task<CommadResponse> Handle(DeletePurposeCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.Id);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeletePurposeCommand, Purpose>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }
}
