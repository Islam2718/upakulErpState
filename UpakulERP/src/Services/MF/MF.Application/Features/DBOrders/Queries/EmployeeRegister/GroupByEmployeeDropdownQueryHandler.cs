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

namespace MF.Application.Features.DBOrders.Queries.EmployeeRegister
{
    public class GroupByEmployeeDropdownQueryHandler : IRequestHandler<GroupByEmployeeDropdownQuery, List<CustomSelectListItem>>
    {
        IGroupWiseEmployeeAssignRepository _repository;
        IMapper _mapper;
        public GroupByEmployeeDropdownQueryHandler(IGroupWiseEmployeeAssignRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CustomSelectListItem>> Handle(GroupByEmployeeDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj = _repository.GetGroupByEmployeeId(request.employeeId);
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "" });
            if (lstObj.Any())
            {
                list.AddRange(lstObj);
            }
            return list;
        }

    }



}
