using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateGroupWiseEmployeeAssignCommand : IRequest<CommadResponse>
    {
        public int? ReleaseEmployeeId { get; set; }
        public int? AssignEmployeeId { get; set; }
        public List<int>? ReleaseGroupListId { get; set; }
        public List<int>? AssignedGroupListId { get; set; }
        public string? JoiningDate { get; set; }
        public string? ReleaseDate { get; set; }
        public string? ReleaseNote { get; set; }

        public int? loginEmployeeId { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }
}
