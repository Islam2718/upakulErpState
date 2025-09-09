using System.Net;
using AutoMapper;
using CommonServices.Enums;
using CommonServices.Repository.Abastract;
using CommonServices.RequestModel;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Contacts.Persistence.Loan;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Domain.Models.Loan;
using Microsoft.Extensions.Configuration;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Handler
{
    public class UpdateLoanApplicationCommandHandler : IRequestHandler<UpdateLoanApplicationCommand, CommadResponse>
    {
        IMapper _mapper;
        ILoanApplicationRepository _repository;
        IMemberRepository _memberRepository;
        IFileService _fileService;
        private IConfiguration _configuration;

        public UpdateLoanApplicationCommandHandler(IMapper mapper, ILoanApplicationRepository repository, IMemberRepository memberRepository, IConfiguration configuration, IFileService fileService)
        {
            _mapper = mapper;
            _repository = repository;
            _memberRepository = memberRepository;
            _configuration = configuration;
            _fileService = fileService;
        }

        public async Task<CommadResponse> Handle(UpdateLoanApplicationCommand request, CancellationToken cancellationToken)
        {
            var _ = _repository.GetById(request.LoanApplicationId);
            var _obj = _mapper.Map<UpdateLoanApplicationCommand, LoanApplication>(request, _);
            #region File
            var location = _configuration.GetValue<string>("FileStorageLocation").ToString();
            if (request.LoneeGroupImg != null)
            {

                if (!string.IsNullOrEmpty(_obj.LoneeGroupImgUrl))
                    _fileService.DeleteImage(location + _obj.LoneeGroupImgUrl);
                var member_obj = _memberRepository.GetById(request.MemberId);
                FileStorageRequest rileRequest = new FileStorageRequest()
                {
                    MemberId = member_obj.MemberCode,
                    FileTypeAllow = FileTypeEnum.Image.ToString(),
                    Location = location,
                    SingleFile = request.LoneeGroupImg
                };
                var status = await _fileService.SingleFileStorage(rileRequest);
                if (status.Success) _obj.LoneeGroupImgUrl = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
            }

            #endregion File
            bool isSuccess = await _repository.UpdateAsync(_obj);
            return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));

        }
    }
}
