using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence.Loan;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Domain.Models.Loan;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Delete.Handler
{
    public class DeleteLoanProposalCommandHandler : IRequestHandler<DeleteLoanProposalCommand, CommadResponse>
    {
        IMapper _mapper;
        ILoanApplicationRepository _repository;
        public DeleteLoanProposalCommandHandler(IMapper mapper, ILoanApplicationRepository rpository)
        {
            _mapper = mapper;
            _repository = rpository;
        }
        public async Task<CommadResponse> Handle(DeleteLoanProposalCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.LoanApplicationId);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeleteLoanProposalCommand, LoanApplication>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }


}
