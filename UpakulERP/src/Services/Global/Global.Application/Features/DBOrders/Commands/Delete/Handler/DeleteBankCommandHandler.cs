using System.Net;
using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Application.Features.DBOrders.Commands.Delete.Command;
using Global.Application.Features.DBOrders.Commands.Update.Command;
using Global.Domain.Models;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace Global.Application.Features.DBOrders.Commands.Delete.Handler
{
    public class DeleteBankCommandHandler : IRequestHandler<DeleteBankCommand, CommadResponse>
    {
        IMapper _mapper;
        IBankRepository _bankRepository;
        public DeleteBankCommandHandler(IMapper mapper, IBankRepository bankRepository)
        {
            _mapper = mapper;
            _bankRepository = bankRepository;
        }
        public async Task<CommadResponse> Handle(DeleteBankCommand request, CancellationToken cancellationToken)
        {
            var bankObj = _bankRepository.GetById(request.BankId);
            if (bankObj != null)
            {
                var obj = _mapper.Map<DeleteBankCommand, Bank>(request, bankObj);
                bool isSuccess = await _bankRepository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }
}
