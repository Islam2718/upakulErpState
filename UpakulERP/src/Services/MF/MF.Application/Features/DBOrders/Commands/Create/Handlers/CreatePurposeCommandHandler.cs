using System.Net;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Domain.Models.Loan;
using Utility.Constants;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Handler
{
    public class CreatePurposeCommandHandler : IRequestHandler<CreatePurposeCommand, CommadResponse>
    {
        IMapper _mapper;
        IPurposeRepository _purposeRepository;
        public CreatePurposeCommandHandler(IMapper mapper, IPurposeRepository purposeRepository)
        {
            _mapper = mapper;
            _purposeRepository = purposeRepository;
        }

        public async Task<CommadResponse> Handle(CreatePurposeCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Purpose>(request);
            bool isSuccess = await _purposeRepository.AddAsync(obj);
            return (isSuccess ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
        }
    }
}
