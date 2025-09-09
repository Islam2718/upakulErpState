using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using Utility.Constants;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.BankAccountMapping
{
    public class BankAccountMappingQueryHandler : IRequestHandler<BankAccountMappingQuery, List<CustomSelectListItem>>
    {
        IBankAccountMappingRepository _repository;
        IMapper _mapper;

        public BankAccountMappingQueryHandler(IBankAccountMappingRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CustomSelectListItem>> Handle(BankAccountMappingQuery request, CancellationToken cancellationToken)
        {
            var lstObj = _repository.GetAll();
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (request.id == 0 ? true : false) });
            if (lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Selected = ((s.BankAccountMappingId == request.id) ? true : false),
                    Text = s.BankAccountNumber ?? "",
                    Value = s.BankAccountMappingId.ToString()
                }));
            }
            return list;
        }

    }


}
