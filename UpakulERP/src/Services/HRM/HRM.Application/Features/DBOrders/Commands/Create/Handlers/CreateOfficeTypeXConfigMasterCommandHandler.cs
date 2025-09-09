using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Create.Command;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using HRM.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Create.Handlers
{

    public class CreateOfficeTypeXConfigMasterCommandHandler : IRequestHandler<CreateOfficeTypeXConfigMasterCommand, CommadResponse>
    {
        IMapper _mapper;
        IOfficeTypeXConfigMasterRepository _repository;
        public CreateOfficeTypeXConfigMasterCommandHandler(IMapper mapper, IOfficeTypeXConfigMasterRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateOfficeTypeXConfigMasterCommand request, CancellationToken cancellationToken)
        {
            bool isSuccess = false;

            var masterId = await _repository.InsertMasterAsync(new OfficeTypeXConfigMaster
            {
                OfficeTypeId = request.OfficeTypeId,
                ApplicantDesignationId = request.ApplicantDesignationId,
                LeaveCategoryId = request.LeaveCategoryId,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.Now
            });

            foreach (var mapping in request.Mappings)
            {
                var detail = new OfficeTypeXConfigureDetails
                {
                    ConfigureMasterId = masterId,
                    ApproverDesignationId = mapping.ApproverDesignationId,
                    LevelNo = mapping.LevelNo,
                    MinimumLeave = mapping.MinimumLeave,
                    MaximumLeave = mapping.MaximumLeave ?? 0,
                    CreatedBy = request.CreatedBy,
                    CreatedOn = DateTime.Now
                };

                isSuccess = await _repository.InsertDetailAsync(detail);
            }
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));

        }
    
    }
}