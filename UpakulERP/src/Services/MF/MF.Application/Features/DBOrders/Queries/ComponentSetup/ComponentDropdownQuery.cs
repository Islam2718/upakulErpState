using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.Component
{
    public class ComponentDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public int? Id { get; set; }
        public int? officeId { get; set; }
        public string ComponentType { get; set; }
        public string? LoanType {  get; set; }
        public ComponentDropdownQuery(int? id,int?officeid, string componentType,string? loanType)
        {
            Id = id;
            officeId = officeid;
            ComponentType = componentType;
            LoanType = loanType;
        }
    }
}