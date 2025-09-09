using System.Net;
using MediatR;
using MF.Application.Contacts.Persistence.Loan;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Domain.Models.Loan;
using Microsoft.EntityFrameworkCore.Internal;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Handlers
{

    public class CreateLoanApprovalListCommandHandler : IRequestHandler<CreateLoanApprovalListCommand, CommadResponse>
    {
        private readonly ILoanApprovalRepository _repository;

        public CreateLoanApprovalListCommandHandler(ILoanApprovalRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateLoanApprovalListCommand request, CancellationToken cancellationToken)
        {
            if (!request.LoanApprovals.Any())
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
            try
            {
                var orderedValues = request.LoanApprovals.OrderBy(i => i.StartingValueAmount).ToList();
                var lst = _repository.GetAll();
                if (!lst.Any())
                    lst = new List<LoanApproval>();
                foreach (var l in orderedValues)
                {
                    if (lst.Where(x => x.Level == l.Level).Any())
                    {
                        var obj = lst.First(x => x.Level == l.Level);
                        obj.DesignationId = l.DesignationId;
                        obj.ApprovalType = l.ApprovalType;
                        obj.StartingValueAmount = l.StartingValueAmount;
                        await _repository.UpdateAsync(obj);
                    }
                    else
                    {
                        LoanApproval obj = new LoanApproval()
                        {
                            ApprovalType = l.ApprovalType,
                            StartingValueAmount = l.StartingValueAmount,
                            DesignationId = l.DesignationId,
                            Level = l.Level,
                        };
                        await _repository.AddAsync(obj);
                    }
                }
                return new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return new CommadResponse(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }




}
