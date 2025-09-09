using System.Net;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence.Loan;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Delete.Handler
{
    public class DeleteLoanApprovalCommandHandler : IRequestHandler<DeleteLoanApprovalCommand, CommadResponse>
    {
        ILoanApprovalRepository _repository;
        public DeleteLoanApprovalCommandHandler(ILoanApprovalRepository rpository)
        {
            _repository = rpository;
        }
        public async Task<CommadResponse> Handle(DeleteLoanApprovalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _repository.Delete(request.Lavel);
                //var obj = _repository.GetById(request.Lavel);
                //await _repository.DeleteAsync(obj);
                //var lst = _repository.GetMany(x => x.Lavel > request.Lavel);
                //int lvl = request.Lavel;
                //foreach (var item in lst)
                //{
                //    item.Level = lvl;
                //    await _repository.UpdateAsync(item);
                //    lvl += 1;
                //}
                return new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                return new CommadResponse(ex.Message, HttpStatusCode.NotFound);
            }
        }
    }

}
