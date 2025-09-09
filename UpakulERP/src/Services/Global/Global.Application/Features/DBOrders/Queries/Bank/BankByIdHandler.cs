using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Domain.ViewModels;
using MediatR;

namespace Global.Application.Features.DBOrders.Queries.Bank
{
   public class BankByIdHandler : IRequestHandler<BankByIdQuery, BankVM>
    {
        IBankRepository _repository;
        IMapper _mapper;
        public BankByIdHandler(IBankRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BankVM> Handle(BankByIdQuery request, CancellationToken cancellationToken)
        {
            var obj =  _repository.GetById(request.id);
            return _mapper.Map<BankVM>(obj);
        }
    }
}
