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
    public class GetGeoLocationHandler: IRequestHandler<GeoLocationDropdownQuery, List<CustomSelectListItem>>
    {
        IGeoLocationRepository geoLocationRepository;
        IMapper _mapper;
        public GetGeoLocationHandler(IGeoLocationRepository geoLocationRepository, IMapper mapper)
        {
            this.geoLocationRepository = geoLocationRepository;
            _mapper = mapper;
        }
        public async Task<List<CustomSelectListItem>> Handle(GeoLocationDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj = await geoLocationRepository.GetGeoLocationByParentId(request.pid);
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (request.pid == 0 ? true : false) });
            if(lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Selected = ((s.GeoLocationId == request.pid) ? true : false),
                    Text = s.GeoLocationCode +" - "+ s.GeoLocationName,
                    Value = s.GeoLocationId.ToString()
                })); 
            }
            return list;
        }
    }
}
