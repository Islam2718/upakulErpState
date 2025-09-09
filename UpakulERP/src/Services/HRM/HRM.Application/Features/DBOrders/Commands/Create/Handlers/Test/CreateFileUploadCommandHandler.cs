using System.Net;
using CommonServices.Enums;
using CommonServices.Repository.Abastract;
using CommonServices.RequestModel;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Create.Commands.Test;
using HRM.Domain.Models.Test;
using MediatR;
using Microsoft.Extensions.Configuration;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Handlers.Test
{
    public class CreateFileUploadCommandHandler : IRequestHandler<CreateFileUploadTestCommand, CommadResponse>
    {
        IFileUploadTestRepository _repository;
        IFileService _fileService;
        private IConfiguration _configuration;
        public CreateFileUploadCommandHandler(IFileUploadTestRepository repository, IFileService fileService, IConfiguration configuration) 
        {
            _repository = repository;
            _fileService = fileService;
            _configuration = configuration;
        }
        public async Task<CommadResponse> Handle(CreateFileUploadTestCommand request, CancellationToken cancellationToken)
        {
            FileStorageRequest rileRequest = new FileStorageRequest()
            {
                FileTypeAllow = FileTypeEnum.Image.ToString(),
                Location = _configuration.GetValue<string>("FileStorageLocation").ToString(),
                MultipleFile = request.formFile,
                
            };
            var rr = await _fileService.MultipleFileStorage(rileRequest);
            if (rr.Success)
            {
                foreach (var r in rr.fileAfterStorageInfos)
                {
                    FileUploadTest obj = new FileUploadTest()
                    {
                        Purpose = request.Purpose,
                        FileExtention = r.FileExtention,
                        FileName = r.FileName,
                        Location = r.FileLocation,
                        OrginalLocation = r.FileOrginalLocation,
                    };
                    bool isSuccess = await _repository.AddAsync(obj);
                }
            }
           
            return (new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created));
        }
    }
}
