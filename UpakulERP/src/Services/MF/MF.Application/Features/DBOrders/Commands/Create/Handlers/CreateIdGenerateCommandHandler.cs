using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Domain.Models;
using System.Net;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Handlers
{
  public  class CreateIdGenerateCommandHandler : IRequestHandler<CreateIdGenerateCommand, CommadResponse>
    {
        IMapper _mapper;
        IIdGeneratorRepository _repository;
        public CreateIdGenerateCommandHandler(IMapper mapper, IIdGeneratorRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateIdGenerateCommand request, CancellationToken cancellationToken)
        {
            //if (_repository.GetMany(c => c.Code == request.ComponentCode).Any())
            //    return new CommadResponse(MessageTexts.duplicate_entry("Component Code"), HttpStatusCode.NotAcceptable);
            //else
            //{
                var obj = _mapper.Map<IdGenerate>(request);
                bool isSuccess = await _repository.AddAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
            }
        }
    }

