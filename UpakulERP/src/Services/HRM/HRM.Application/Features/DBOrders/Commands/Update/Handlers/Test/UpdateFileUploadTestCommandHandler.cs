using AutoMapper;
using CommonServices.Repository.Abastract;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Update.Commands.Test;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Update.Handler.Test
{
    public class UpdateFileUploadTestCommandHandler : IRequestHandler<UpdateFileUploadTestCommand, CommadResponse>
    {
        IMapper _mapper;
        IFileUploadTestRepository _repository;
        IFileService _fileService;
        private IConfiguration _configuration;
        public UpdateFileUploadTestCommandHandler(IMapper mapper, IFileUploadTestRepository repository, IFileService fileService, IConfiguration configuration)
        {
            _mapper = mapper;
            _repository = repository;
            _fileService = fileService;
            _configuration = configuration;
        }

        public async Task<CommadResponse> Handle(UpdateFileUploadTestCommand request, CancellationToken cancellationToken)
        {

            var _obj = await _repository.GetById(request.Id);
            _fileService.DeleteImage(_obj.OrginalLocation +  _obj.FileName);
            bool isSuccess = await _repository.UpdateAsync(_obj);
            return (isSuccess ? new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.update_failed, HttpStatusCode.BadRequest));
        }
    }
}

