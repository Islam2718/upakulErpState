using Accounts.Application.Contacts.Persistence;
using AutoMapper;
using MediatR;

namespace Accounts.Application.Features.DBOrders.Queries.BudgetComponent
{
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, BudgetComponentVM>
    {
        IBudgetComponentRepository _repository;
        IMapper _mapper;
        public GetByIdHandler(IBudgetComponentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BudgetComponentVM> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return _mapper.Map<BudgetComponentVM>(obj);
        }
    }




}
