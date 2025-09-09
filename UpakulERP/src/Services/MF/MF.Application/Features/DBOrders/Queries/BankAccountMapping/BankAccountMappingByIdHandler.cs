using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.BankAccountMapping
{
    class BankAccountMappingByIdHandler : IRequestHandler<BankAccountMappingByIdQuery, BankAccountMappingVM>
    {
        IBankAccountMappingRepository _repository;
        IMapper _mapper;
        public BankAccountMappingByIdHandler(IBankAccountMappingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BankAccountMappingVM> Handle(BankAccountMappingByIdQuery request, CancellationToken cancellationToken)
        {
            //Check Exists Data
            //then
            //if (_repository.GetMany(c => c.AccountNumber == request.AccountNumber).Any())
            //    return new CommadResponse(MessageTexts.duplicate_entry("Duplicate Account Number"), HttpStatusCode.NotAcceptable);
            //else
            //{
                var obj = _repository.GetById(request.id);
                return _mapper.Map<BankAccountMappingVM>(obj);
            //}
        }
    }
}
