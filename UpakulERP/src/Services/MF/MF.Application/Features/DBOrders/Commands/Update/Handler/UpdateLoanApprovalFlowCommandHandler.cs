using AutoMapper;
using CommonServices.Repository.Abastract;
using MediatR;
using MF.Application.Contacts.Enums;
using MF.Application.Contacts.Persistence;
using MF.Application.Contacts.Persistence.Loan;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Domain.Models.Loan;
using System.Net;
using Utility.Constants;
using Utility.Domain;
using Utility.Enums;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Handler
{
    public class UpdateLoanApprovalFlowCommandHandler : IRequestHandler<UpdateLoanApprovalFlowCommand, CommadResponse>
    {
        IMapper _mapper;
        IMemberRepository _memberRepository;
        ILoanApplicationRepository _repository;
        ILoanApprovalRepository _approval_repository;
        ILoanSummaryRepository _summaryRepository;
        IComponentRepository _componentRepository;
        IDailyProcessRepository _dailyProcessRepository;
        IConverterService _converterService;

        public UpdateLoanApprovalFlowCommandHandler(IMapper mapper, IMemberRepository memberRepository, ILoanApplicationRepository repository
            , ILoanApprovalRepository approval_repository, ILoanSummaryRepository summaryRepository
            , IComponentRepository componentRepository, IDailyProcessRepository dailyProcessRepository
            , IConverterService converterService)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _repository = repository;
            _approval_repository = approval_repository;
            _summaryRepository = summaryRepository;
            _componentRepository = componentRepository;
            _dailyProcessRepository = dailyProcessRepository;
            _converterService = converterService;
        }

        public async Task<CommadResponse> Handle(UpdateLoanApprovalFlowCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.LoanApplicationId);

            if (_obj == null)
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
            else if (_obj.ApplicationStatus == LoanApproveStatus.Disbursed)
                return new CommadResponse("Already disbursed", HttpStatusCode.BadRequest);

            if (request.ActionType == "APPROVED")
            {
                if (_obj.ApplicationStatus == LoanApproveStatus.ReadyForDisbursed)
                    return new CommadResponse("Waiting for disburse", HttpStatusCode.BadRequest);
                string ap_sts = _obj.ApplicationStatus == LoanApproveStatus.Pending ? "C" : _obj.ApplicationStatus == LoanApproveStatus.Approved ? LoanApproveStatus.Approved : "";
                var status = _approval_repository.IsValidEmployee(request.loggedInEmpId ?? 0, ((_obj.ApprovedLevel ?? 0) + 1), ap_sts, request.ProposedAmount);
                if (!status) return new CommadResponse("You are not permitted", HttpStatusCode.Forbidden);
                #region
                if ((_obj.ApprovedLevel ?? 0) == 0) /*Checker*/
                {
                    _obj.CheckedBy = request.loggedInEmpId;
                    _obj.CheckedDate = DateTime.Now;
                    _obj.CheckerProposedAmount = request.ProposedAmount;
                }
                else if (_obj.ApprovedLevel == 1) /*Branch mamager*/
                {
                    _obj.FirstApprovedBy = request.loggedInEmpId;
                    _obj.FirstApprovedOn = DateTime.Now;
                    _obj.FirstApprovedAmount = request.ProposedAmount;
                }
                else if (_obj.ApprovedLevel == 2) /*Area Manager*/
                {
                    _obj.SecondApprovedBy = request.loggedInEmpId;
                    _obj.SecondApprovedOn = DateTime.Now;
                    _obj.SecondApprovedAmount = request.ProposedAmount;
                }
                else if (_obj.ApprovedLevel == 3) /*Rejonal Manager*/
                {
                    _obj.ThirdApprovedBy = request.loggedInEmpId;
                    _obj.ThirdApprovedOn = DateTime.Now;
                    _obj.ThirdApprovedAmount = request.ProposedAmount;
                }
                else if (_obj.ApprovedLevel == 4) /*Zonal Manager*/
                {
                    _obj.FourthApprovedBy = request.loggedInEmpId;
                    _obj.FourthApprovedOn = DateTime.Now;
                    _obj.FourthApprovedAmount = request.ProposedAmount;
                }
                #endregion
                _obj.ApprovedLevel = (_obj.ApprovedLevel ?? 0) + 1;
                var next_status = _repository.GetNextApprovalStatus(_obj.LoanApplicationId, _obj.ApprovedLevel.Value, request.ProposedAmount);
                if (next_status != null)
                {
                    if (next_status.ApprovalType == null || next_status.Level == null)
                        return new CommadResponse("You are not permitted", HttpStatusCode.Forbidden);
                    else
                    {
                        _obj.ApplicationStatus = next_status.ApprovalType /*== "D" ? LoanApproveStatus.ReadyForDisbursed : LoanApproveStatus.Approved*/;
                        //_obj.ApprovedLevel = next_status.Level;
                        await _repository.UpdateAsync(_obj);
                        return new CommadResponse(MessageTexts.approved_success, HttpStatusCode.Accepted);
                    }
                }
                else
                    return new CommadResponse("You are not permitted", HttpStatusCode.Forbidden);
            }
            else if (request.ActionType == "REJECTED")
            {
                _obj.ApplicationStatus = LoanApproveStatus.Reject;
                _obj.RejectedBy = request.loggedInEmpId;
                _obj.RejectedOn = DateTime.Now;
                _obj.RejectedNote = request.Note;
                await _repository.UpdateAsync(_obj);
                return new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted);
            }
            else if (request.ActionType == "REVISED")
            {
                _obj.ApplicationStatus = LoanApproveStatus.Revised;
                _obj.RevisedBy = request.loggedInEmpId;
                _obj.RevisedOn = DateTime.Now;
                _obj.RevisedNote = request.Note;
                _obj.ApprovedLevel = 0;
                await _repository.UpdateAsync(_obj);
                return new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted);
            }
            else if (request.ActionType == "DISBURSED")
            {
                if (request.loggedInOfficeId != _obj.OfficeId)
                    return new CommadResponse("Office information does not match.", HttpStatusCode.Forbidden);
                else if (request.loggedInOfficeTypeId != (int)OfficeType.OfficeTypeEnum.Branch)
                    return new CommadResponse("Distribution is permitted only through the branch office.", HttpStatusCode.Forbidden);

                if (!request.transactionDate.HasValue)
                {
                    var tr_dt_pr = _dailyProcessRepository.GetMany(x => !x.IsDayClose && x.OfficeId == request.loggedInOfficeId && x.EndDate == null);
                    if (tr_dt_pr.Any())
                        request.transactionDate = tr_dt_pr.First().TransactionDate;
                }
                if (!request.transactionDate.HasValue)
                    return new CommadResponse(MessageTexts.transaction_date_required, HttpStatusCode.BadRequest);
                else
                {

                    var com_obj = _componentRepository.GetById(_obj.ComponentId);

                    _obj.DistributedAmount = request.ProposedAmount;
                    _obj.DistributedBy = request.loggedInEmpId;
                    _obj.DistributedOn = DateTime.Now;
                    _obj.ApplicationStatus = LoanApproveStatus.Disbursed;
                    LoanSummary loanSummary = new LoanSummary()
                    {
                        CompulsoryGeneralSavingSummaryId = 0,
                        OpeningGeneralSavingSummaryId = 0,

                        DisburseDate = request.transactionDate.Value,
                        GracePeriod = com_obj.GracePeriodInDay ?? 0,
                        InterestRate = com_obj.InterestRate / 100,
                        LoanComponentId = _obj.ComponentId,
                        IsActive = true,
                        LoanApplicationId = request.LoanApplicationId,
                        LoanPurposeId = _obj.PurposeId,
                        OfficeId = _obj.OfficeId,
                        MemberId = _obj.MemberId,
                        GroupId = _obj.GroupId,
                        PaymentFrequency = com_obj.PaymentFrequency,
                        PaymentType = request.PaymentType,
                        PrincipleAmount = request.ProposedAmount,
                        CreatedBy = request.loggedInEmpId,
                        CreatedOn = DateTime.Now,
                        BankAccountMappingId = request.BankId,
                        ChequeNo = request.ChequeNo,
                        ReferenceNo = request.ReferenceNo
                    };
                    var status = _summaryRepository.NewDisbursed(loanSummary, com_obj.DurationInMonth ?? 0, com_obj.NoOfInstalment ?? 0);
                    if (status)
                    {
                        await _repository.UpdateAsync(_obj);
                        if (request.PaymentType == PaymentType.Bearer_Cheque || request.PaymentType == PaymentType.Account_Pay_Cheque)
                        {
                            int[] day_arr = (loanSummary.DisburseDate.Day < 10 ? $"0{loanSummary.DisburseDate.Day}" : loanSummary.DisburseDate.Day.ToString()).Select(c => int.Parse(c.ToString())).ToArray();
                            int[] month_arr = (loanSummary.DisburseDate.Month < 10 ? $"0{loanSummary.DisburseDate.Month}" : loanSummary.DisburseDate.Month.ToString()).Select(c => int.Parse(c.ToString())).ToArray();
                            int[] year_arr = loanSummary.DisburseDate.Year.ToString().Select(c => int.Parse(c.ToString())).ToArray();
                            var memObj = _memberRepository.GetById(_obj.MemberId);
                            var chkNote = new ChequeModel
                            {
                                PayTo = $"{memObj.MemberCode} - {memObj.MemberName}{(request.PaymentType == PaymentType.Account_Pay_Cheque ? $" Acc: {request.ReferenceNo}" : "")}",
                                Acc_Pay = (request.PaymentType == PaymentType.Account_Pay_Cheque ? "Account Pay Only" : ""),
                                Amount = _obj.DistributedAmount.Value,
                                InWord = _converterService.NumberToWordConvert((decimal)_obj.DistributedAmount.Value),
                                Day_1 = day_arr[0],
                                Day_2 = day_arr[1],
                                Month_1 = month_arr[0],
                                Month_2 = month_arr[1],
                                year_1 = year_arr[0],
                                year_2 = year_arr[1],
                                year_3 = year_arr[2],
                                year_4 = year_arr[3],
                            };

                            return new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted, Returnobj: chkNote);
                        }
                        else return new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted);
                    }
                    else
                        return new CommadResponse(MessageTexts.approved_failed, HttpStatusCode.ExpectationFailed);
                }
            }
            else
                return new CommadResponse("The requested action type is not permitted.", HttpStatusCode.Forbidden);
        }
    }


}
