using System.Net;
using AutoMapper;
using CommonServices.Enums;
using CommonServices.Repository.Abastract;
using CommonServices.RequestModel;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Domain.Models;
using Microsoft.Extensions.Configuration;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Handler
{
    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, CommadResponse>
    {
        IMapper _mapper;
        IFileService _fileService;
        IMemberRepository _repository;
        private IConfiguration _configuration;
        public UpdateMemberCommandHandler(IMapper mapper, IMemberRepository repository, IConfiguration configuration, IFileService fileService)
        {
            _mapper = mapper;
            _repository = repository;
            _configuration = configuration;
            _fileService = fileService;
        }

        public async Task<CommadResponse> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.MemberId);

            #region File
            var location = _configuration.GetValue<string>("FileStorageLocation").ToString();
            if (request.MemberImg != null)
            {
                if (!string.IsNullOrEmpty(_obj.MemberImgUrl))
                    _fileService.DeleteImage(location + _obj.MemberImgUrl);

                FileStorageRequest rileRequest = new FileStorageRequest()
                {
                    FileTypeAllow = FileTypeEnum.Image.ToString(),
                    Location = location,
                    SingleFile = request.MemberImg,
                    MemberId = _obj.MemberCode,
                    MaxFileSize = 1 * 1024 * 1024 // 1mb
                };
                var status = await _fileService.SingleFileStorage(rileRequest);
                if (status.Success) _obj.MemberImgUrl = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
            }

            if (request.SignatureImg != null)
            {
                if (!string.IsNullOrEmpty(_obj.SignatureImgUrl))
                    _fileService.DeleteImage(location + _obj.SignatureImgUrl);

                FileStorageRequest rileRequest = new FileStorageRequest()
                {
                    FileTypeAllow = FileTypeEnum.Image.ToString(),
                    Location = location,
                    SingleFile = request.SignatureImg,
                    MemberId = _obj.MemberCode,
                    MaxFileSize = 1 * 1024 * 1024 // 1mb
                };
                var status = await _fileService.SingleFileStorage(rileRequest);
                if (status.Success) _obj.SignatureImgUrl = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
            }

            if (request.NidFrontImg != null)
            {
                if (!string.IsNullOrEmpty(_obj.NidFrontImgUrl))
                    _fileService.DeleteImage(location + _obj.NidFrontImgUrl);
                FileStorageRequest rileRequest = new FileStorageRequest()
                {
                    FileTypeAllow = FileTypeEnum.Image.ToString(),
                    Location = location,
                    SingleFile = request.NidFrontImg,
                    MemberId = _obj.MemberCode,
                    MaxFileSize = 1 * 1024 * 1024 // 1mb
                };
                var status = await _fileService.SingleFileStorage(rileRequest);
                if (status.Success) _obj.NidFrontImgUrl = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
            }
            if (request.NidBackImg != null)
            {
                if (!string.IsNullOrEmpty(_obj.NidBackImgUrl))
                    _fileService.DeleteImage(location + _obj.NidBackImgUrl);
                FileStorageRequest rileRequest = new FileStorageRequest()
                {
                    FileTypeAllow = FileTypeEnum.Image.ToString(),
                    Location = location,
                    SingleFile = request.NidBackImg,
                    MemberId = _obj.MemberCode,
                    MaxFileSize = 1 * 1024 * 1024 // 1mb
                };
                var status = await _fileService.SingleFileStorage(rileRequest);
                if (status.Success) _obj.NidBackImgUrl = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
            }
            #endregion File
            var obj = _mapper.Map<UpdateMemberCommand, Member>(request, _obj);
            bool isSuccess = await _repository.UpdateAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
        }
    }
}
