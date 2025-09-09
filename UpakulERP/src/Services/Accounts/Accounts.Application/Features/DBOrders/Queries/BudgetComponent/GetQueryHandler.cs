using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Accounts.Application.Contacts.Persistence;
using AutoMapper;
using MediatR;
using Utility.Constants;
using Utility.Domain;

namespace Accounts.Application.Features.DBOrders.Queries.BudgetComponent
{
    public class GetQueryHandler : IRequestHandler<GetQuery, List<CustomSelectListItem>>
    {
        IBudgetComponentRepository _repository;
        IMapper _mapper;
        public GetQueryHandler(IBudgetComponentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            var lstObj = await _repository.GetComponentForDropdown(request.pid);
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (request.pid == 0 ? true : false) });
            if (lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Selected = ((s.Id == request.pid) ? true : false),
                    Text = s.ComponentName,
                    Value = s.Id.ToString()
                }));
            }
            return list;
        }
    }



}
