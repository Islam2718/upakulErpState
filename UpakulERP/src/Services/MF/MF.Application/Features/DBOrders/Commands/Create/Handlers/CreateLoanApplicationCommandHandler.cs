using System.Net;
using AutoMapper;
using CommonServices.Enums;
using CommonServices.Repository.Abastract;
using CommonServices.RequestModel;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using Microsoft.Extensions.Configuration;
using Utility.Constants;
using Utility.Response;
using MF.Domain.Models.Loan;
using MF.Application.Contacts.Persistence.Loan;
using MF.Application.Contacts.Enums;

namespace MF.Application.Features.DBOrders.Commands.Create.Handlers
{

    public class CreateLoanApplicationCommandHandler : IRequestHandler<CreateLoanApplicationCommand, CommadResponse>
    {
        //#
        IMapper _mapper;
        ILoanApplicationRepository _repository;
        IMemberRepository _memberRepository;
        IFileService _fileService;
        private IConfiguration _configuration;

        public CreateLoanApplicationCommandHandler(IMapper mapper, ILoanApplicationRepository repository, IMemberRepository memberRepository, IConfiguration configuration, IFileService fileService)
        {
            _mapper = mapper;
            _repository = repository;
            _memberRepository = memberRepository;
            _configuration = configuration;
            _fileService = fileService;
        }

        public async Task<CommadResponse> Handle(CreateLoanApplicationCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<LoanApplication>(request);
            obj.ApplicationStatus = LoanApproveStatus.Pending;

            #region Image
            var location = _configuration.GetValue<string>("FileStorageLocation").ToString();
            if (request.LoneeGroupImg != null)
            {
                var member_obj = _memberRepository.GetById(request.MemberId);
                FileStorageRequest rileRequest = new FileStorageRequest()
                {
                    FileTypeAllow = FileTypeEnum.Image.ToString(),
                    MemberId = member_obj.MemberCode,
                    Location = location,
                    SingleFile = request.LoneeGroupImg,
                    MaxFileSize = 1 * 1024 * 1024, // 1mb
                };
                var status = await _fileService.SingleFileStorage(rileRequest);
                if (status.Success)
                    obj.LoneeGroupImgUrl = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
            }
            #endregion
            obj.ApplicationNo=DateTime.Now.ToString("yyyyMMdd");
            bool isSuccess = await _repository.AddAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
        }
    }
}
