using System.Net;
using AutoMapper;
using CommonServices.Enums;
using CommonServices.Repository.Abastract;
using CommonServices.RequestModel;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;
using HRM.Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Update.Handlers
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, CommadResponse>
    {
        IMapper _mapper;
        IFileService _fileService;
        IEmployeeRepository _repository;
        private IConfiguration _configuration;

        public UpdateEmployeeCommandHandler
            (IMapper mapper, IFileService fileService, IEmployeeRepository repository, IConfiguration configuration)
        {
            _mapper = mapper;
            _fileService = fileService;
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<CommadResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.EmployeeCode == request.EmployeeCode && c.EmployeeId != request.EmployeeId).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("File no"), HttpStatusCode.NotAcceptable);
            else
            {
                var location = _configuration.GetValue<string>("FileStorageLocation").ToString();
                var _obj = await _repository.GetById(request.EmployeeId);
                #region File
                if (request.EmployeePic != null)
                {
                    if (!string.IsNullOrEmpty(_obj.EmployeePicURL))
                        _fileService.DeleteImage(location + _obj.EmployeePicURL);

                    FileStorageRequest rileRequest = new FileStorageRequest()
                    {
                        FileTypeAllow = FileTypeEnum.Image.ToString(),
                        Location = location,
                        SingleFile = request.EmployeePic,
                        EmployeeId = request.EmployeeCode,
                        MaxFileSize = 5 * 1024 * 1024 // 5mb
                    };
                    var status = await _fileService.SingleFileStorage(rileRequest);
                    if (status.Success) request.EmployeePicURL = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                    else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
                }

                if (request.EmpSignature != null)
                {
                    if (!string.IsNullOrEmpty(_obj.EmpSignatureUrl))
                        _fileService.DeleteImage(location + _obj.EmpSignatureUrl);

                    FileStorageRequest rileRequest = new FileStorageRequest()
                    {
                        FileTypeAllow = FileTypeEnum.Image.ToString(),
                        Location = location,
                        SingleFile = request.EmpSignature,
                        EmployeeId = request.EmployeeCode,
                        MaxFileSize = 5 * 1024 * 1024 // 5mb
                    };
                    var status = await _fileService.SingleFileStorage(rileRequest);
                    if (status.Success) request.EmpSignatureUrl = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                    else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
                }

                if (request.NIDPic != null)
                {
                    if (!string.IsNullOrEmpty(_obj.NIDPicUrl))
                        _fileService.DeleteImage(location + _obj.NIDPicUrl);
                    FileStorageRequest rileRequest = new FileStorageRequest()
                    {
                        FileTypeAllow = FileTypeEnum.Image.ToString(),
                        Location = location,
                        SingleFile = request.NIDPic,
                        EmployeeId = request.EmployeeCode,
                        MaxFileSize = 5 * 1024 * 1024 // 5mb
                    };
                    var status = await _fileService.SingleFileStorage(rileRequest);
                    if (status.Success) request.NIDPicUrl = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                    else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
                }

                if (request.SpousePic != null)
                {
                    if (!string.IsNullOrEmpty(_obj.SpousePicURL))
                        _fileService.DeleteImage(location + _obj.SpousePicURL);
                    FileStorageRequest rileRequest = new FileStorageRequest()
                    {
                        FileTypeAllow = FileTypeEnum.Image.ToString(),
                        Location = location,
                        SingleFile = request.SpousePic,
                        EmployeeId = request.EmployeeCode,
                        //Module = ModuleShortFormEnum.HRM.ToString(),
                    };
                    var status = await _fileService.SingleFileStorage(rileRequest);
                    if (status.Success) request.SpousePicURL = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                    else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
                }
                #endregion File
                var obj = _mapper.Map<UpdateEmployeeCommand, Employee>(request, _obj);

                bool isSuccess = await _repository.UpdateAsync(obj);

                return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted, ReturnId: obj.EmployeeId) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
            }
        }
    }
}

