using System.Net;
using AutoMapper;
using CommonServices.Enums;
using CommonServices.Repository.Abastract;
using CommonServices.RequestModel;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using HRM.Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CommadResponse>
    {
        IMapper _mapper;
        IFileService _fileService;
        IEmployeeRepository _repository;
        private IConfiguration _configuration;

        public CreateEmployeeCommandHandler
            (IMapper mapper, IFileService fileService, IEmployeeRepository repository, IConfiguration configuration)
        {
            _mapper = mapper;
            _fileService = fileService;
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<CommadResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetMany(c => c.EmployeeCode == request.EmployeeCode).Any())
                return new CommadResponse(MessageTexts.duplicate_entry("File no"), HttpStatusCode.NotAcceptable);
            else
            {
                #region File
                var location = _configuration.GetValue<string>("FileStorageLocation").ToString();
                if (request.EmployeePic != null)
                {
                    FileStorageRequest rileRequest = new FileStorageRequest()
                    {
                        FileTypeAllow = FileTypeEnum.Image.ToString(),
                        Location = location,
                        SingleFile = request.EmployeePic,
                        EmployeeId = request.EmployeeCode,
                        MaxFileSize = 5 * 1024 * 1024, // 5mb
                    };
                    var status =await  _fileService.SingleFileStorage(rileRequest);
                    if (status.Success)
                        request.EmployeePicURL = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                    else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
                }
                if (request.EmpSignature != null)
                {
                    FileStorageRequest rileRequest = new FileStorageRequest()
                    {
                        FileTypeAllow = FileTypeEnum.Image.ToString(),
                        Location = location,
                        SingleFile = request.EmpSignature,
                        EmployeeId = request.EmployeeCode,
                        MaxFileSize = 5 * 1024 * 1024 // 5mb
                    };
                    var status = await _fileService.SingleFileStorage(rileRequest);
                    if (status.Success)
                        request.EmpSignatureUrl = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                    else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
                }
                if (request.NIDPic != null)
                {
                    FileStorageRequest rileRequest = new FileStorageRequest()
                    {
                        FileTypeAllow = FileTypeEnum.Image.ToString(),
                        Location = location,
                        SingleFile = request.NIDPic,
                        EmployeeId = request.EmployeeCode,
                        MaxFileSize = 5 * 1024 * 1024 // 5mb
                    };
                    var status = await _fileService.SingleFileStorage(rileRequest);
                    if (status.Success)
                        request.NIDPicUrl = status.fileAfterStorageInfos[0].FileLocation + "/" + status.fileAfterStorageInfos[0].FileName;
                    else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
                }
                if (request.SpousePic != null)
                {
                    FileStorageRequest rileRequest = new FileStorageRequest()
                    {
                        FileTypeAllow = FileTypeEnum.Image.ToString(),
                        Location = location,
                        SingleFile = request.SpousePic,
                        EmployeeId = request.EmployeeCode,
                        MaxFileSize = 5 * 1024 * 1024 // 5mb
                    };
                    var status = await _fileService.SingleFileStorage(rileRequest);
                    if (status.Success)
                        request.SpousePicURL = status.fileAfterStorageInfos[0].FileLocation+"/" + status.fileAfterStorageInfos[0].FileName;
                    else return new CommadResponse(status.Message, HttpStatusCode.BadRequest);
                }
                #endregion File
                var obj = _mapper.Map<Employee>(request);
                bool isSuccess = await _repository.AddAsync(obj);

                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created, ReturnId: obj.EmployeeId) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }
        }
    }
}
