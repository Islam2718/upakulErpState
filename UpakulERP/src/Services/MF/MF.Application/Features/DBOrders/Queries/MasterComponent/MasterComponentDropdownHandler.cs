using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.MasterComponent
{
   public class MasterComponentDropdownHandler : IRequestHandler<MasterComponentDropdownQuery, List<CustomSelectListItem>>
    {
        IMasterComponentRepository _repository;
        IMapper _mapper;

        public MasterComponentDropdownHandler(IMasterComponentRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CustomSelectListItem>> Handle(MasterComponentDropdownQuery request, CancellationToken cancellationToken)
        {
            var list = new List<CustomSelectListItem>();

            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = true });

            list.AddRange(_repository.GetAll().ToList().Select(s => new CustomSelectListItem
            {
                Selected = false,
                Text = s.Name,
                Value = s.Id.ToString()
            }));
            return list;
        }

    }
}
