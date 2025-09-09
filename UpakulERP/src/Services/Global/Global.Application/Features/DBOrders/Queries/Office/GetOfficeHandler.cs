using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Global.Application.Contacts.Persistence;
using Global.Domain.Models;
using MediatR;
using Newtonsoft.Json.Linq;
using Utility.Constants;
using Utility.Domain;

namespace Global.Application.Features.DBOrders.Queries.GeoLocation
{
    public class GetOfficeHandler : IRequestHandler<OfficeQuery, List<CustomSelectListItem>>
    {
        IOfficeRepository _repository;
        IMapper _mapper;
        public GetOfficeHandler(IOfficeRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(OfficeQuery request, CancellationToken cancellationToken)
        {
            var lstObj = await _repository.GetOfficeByParentId(request.pid);
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (request.pid == 0 ? true : false) });
            if (lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Selected = ((s.OfficeId == request.pid) ? true : false),
                    Text = s.OfficeCode + " - " + s.OfficeName,
                    Value = s.OfficeId.ToString()
                }));
            }
            return list;
        }
    }
}
