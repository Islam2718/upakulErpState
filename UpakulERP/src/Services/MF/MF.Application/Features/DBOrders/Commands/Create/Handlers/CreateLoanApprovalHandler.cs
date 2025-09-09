using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence.Loan;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Domain.Models.Loan;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateLoanApprovalHandler : IRequestHandler<CreateLoanApprovalCommand, CommadResponse>
    {
        //#
        IMapper _mapper;
        ILoanApprovalRepository _repository;
        public CreateLoanApprovalHandler(IMapper mapper, ILoanApprovalRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateLoanApprovalCommand request, CancellationToken cancellationToken)
        {
            //if (_repository.GetMany(c => c.MemberCode == request.MemberCode).Any())
            //    return new CommadResponse(MessageTexts.duplicate_entry("Member code"), HttpStatusCode.NotAcceptable);
            //else
            //{
            var obj = _mapper.Map<LoanApproval>(request);
            bool isSuccess = await _repository.AddAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            //}
        }



    }
}
