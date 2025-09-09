using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounts.Application.Contacts.Persistence;
using Accounts.Domain.Models;
using Accounts.Domain.ViewModel;
using AutoMapper;
using MediatR;

namespace Accounts.Application.Features.DBOrders.Queries.BudgetEntry
{
    public class GetRowQueryHandler : IRequestHandler<GetRowQuery, List<BudgetComponentData>>
    {
        IBudgetEntryRepository _repository;
        IMapper _mapper;
        public GetRowQueryHandler(IBudgetEntryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //public async Task<BudgetEntryComponentVM> Handle(GetRowQuery request, CancellationToken cancellationToken)
        //{
        //    var obj = _repository.GetBudgetComponents(request.FinancialYear, request.OfficeId, request.ComponentId, request.ComponentParentId);
        //    return _mapper.Map<BudgetEntryComponentVM>(obj);
        //}

        public async Task<List<BudgetComponentData>> Handle(GetRowQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetAllBudgetComponents(request.FinancialYear, request.OfficeId, request.ComponentParentId);  //, request.ComponentId
            return obj;
        }


    }


}
