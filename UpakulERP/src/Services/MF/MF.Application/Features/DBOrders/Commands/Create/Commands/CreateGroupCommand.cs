using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Commands
{
    public class CreateGroupCommand : IRequest<CommadResponse>
    {
        public int OfficeId { get; set; }
        //public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string GroupType { get; set; }
        public string ScheduleType { get; set; }
        public int? MeetingDay { get; set; }
        public string OpeninigDate { get; set; }
        public string StartDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string? MeetingPlace { get; set; }
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public int UpazilaId { get; set; }
        public int UnionId { get; set; }
        public int VillageId { get; set; }
        public string? Address { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now; 
    }
}
