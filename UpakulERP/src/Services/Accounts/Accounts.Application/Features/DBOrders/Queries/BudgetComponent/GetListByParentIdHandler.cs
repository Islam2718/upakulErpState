using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounts.Application.Contacts.Persistence;
using AutoMapper;
using MediatR;
using Utility.Constants;
using Utility.Domain;

namespace Accounts.Application.Features.DBOrders.Queries.BudgetComponent
{
    public class GetListByParentIdHandler : IRequestHandler<GetListByParentIdQuery, List<BudgetComponentVM>>
    {
        IBudgetComponentRepository _repository;
        IMapper _mapper;
        public GetListByParentIdHandler(IBudgetComponentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<BudgetComponentVM>> Handle(GetListByParentIdQuery request, CancellationToken cancellationToken)
        {
            var lstObj = await _repository.GetComponentListByParentId(request.pid);
            return _mapper.Map<List<BudgetComponentVM>>(lstObj);
        }
    }



}
