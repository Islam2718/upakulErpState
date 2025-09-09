using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Application.Features.DBOrders.Queries.Bank;
using MediatR;
using Utility.Constants;
using Utility.Domain;

namespace Global.Application.Features.DBOrders.Queries.Office
{
    public class GetOfficeDropdownHandler : IRequestHandler<OfficeDropdownQuery, List<CustomSelectListItem>>
    {
        IOfficeRepository _repository;
        IMapper _mapper;

        public GetOfficeDropdownHandler(IOfficeRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CustomSelectListItem>> Handle(OfficeDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj = _repository.GetOfficeDropdown(request.officeId, request.officeType.Value);
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (request.officeId == 0 ? true : false) });
            if (lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Selected = ((s.OfficeId == request.officeId) ? true : false),
                    Text = s.OfficeCode + " - " + s.OfficeName,
                    Value = s.OfficeId.ToString()
                }));
            }
            return list;
        }
    }
}
