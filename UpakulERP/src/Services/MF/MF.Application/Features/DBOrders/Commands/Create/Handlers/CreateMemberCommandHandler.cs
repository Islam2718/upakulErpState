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

namespace MF.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, CommadResponse>
    {
        IMapper _mapper;
        IMemberRepository _repository;
        IFileService _fileService;
        private IConfiguration _configuration;
        public CreateMemberCommandHandler(IMapper mapper, IMemberRepository repository, IConfiguration configuration, IFileService fileService)
        {
            _mapper = mapper;
            _repository = repository;
            _configuration = configuration;
            _fileService = fileService;
        }

        public async Task<CommadResponse> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var varifyMsg = _repository.MemberDataCheck(request.NationalId, request.SmartCard, request.ContactNoOwn);
            if (varifyMsg != "")
                return new CommadResponse(varifyMsg, HttpStatusCode.NotAcceptable);
            else
            {
                string memberCode = DateTime.Now.ToString("yyddMMHHmmcmd");
                var obj = _mapper.Map<Domain.Models.Member>(request);
                obj.MemberCode = memberCode;
                #region Image
                var location = _configuration.GetValue<string>("FileStorageLocation").ToString();
                if (request.MemberImg != null)
                {
                    FileStorageRequest rileRequest = new FileStorageRequest()
                    {
                        FileTypeAllow = FileTypeEnum.Image.ToString(),
                        Location = location,
                        SingleFile = request.MemberImg,
                        MemberId = memberCode,
                        MaxFileSize = 1 * 1024 * 1024, // 1mb
                    };
                    var status = await _fileService.SingleFileStorage(rileRequest);
                    if (status.Success)
                        obj.MemberImgUrl = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                    else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
                }
                if (request.SignatureImg != null)
                {
                    FileStorageRequest rileRequest = new FileStorageRequest()
                    {
                        FileTypeAllow = FileTypeEnum.Image.ToString(),
                        Location = location,
                        SingleFile = request.SignatureImg,
                        MemberId = memberCode,
                        MaxFileSize = 1 * 1024 * 1024, // 1mb
                    };
                    var status = await _fileService.SingleFileStorage(rileRequest);
                    if (status.Success)
                        obj.SignatureImgUrl = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                    else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
                }
                if (request.NidFrontImg != null)
                {
                    FileStorageRequest rileRequest = new FileStorageRequest()
                    {
                        FileTypeAllow = FileTypeEnum.Image.ToString(),
                        Location = location,
                        SingleFile = request.NidFrontImg,
                        MemberId = memberCode,
                        MaxFileSize = 1 * 1024 * 1024, // 1mb
                    };
                    var status = await _fileService.SingleFileStorage(rileRequest);
                    if (status.Success)
                        obj.NidFrontImgUrl = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                    else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
                }
                if (request.NidBackImg != null)
                {
                    FileStorageRequest rileRequest = new FileStorageRequest()
                    {
                        FileTypeAllow = FileTypeEnum.Image.ToString(),
                        Location = location,
                        SingleFile = request.NidBackImg,
                        MemberId = memberCode,
                        MaxFileSize = 1 * 1024 * 1024, // 1mb
                    };
                    var status = await _fileService.SingleFileStorage(rileRequest);
                    if (status.Success)
                        obj.NidBackImgUrl = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                    else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
                }
                #endregion

                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }
        }

    }



}
