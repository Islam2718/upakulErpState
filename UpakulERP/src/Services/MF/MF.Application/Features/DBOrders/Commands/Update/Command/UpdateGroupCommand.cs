using MediatR;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Command
{
    public class UpdateGroupCommand : IRequest<CommadResponse>
    {
        public int GroupId { get; set; }
        public int OfficeId { get; set; }
        public string GroupName { get; set; }
       // public string GroupCode { get; set; }
        public string GroupType { get; set; }
        public string ScheduleType { get; set; }
        public int? MeetingDay { get; set; }
        public string OpeninigDate { get; set; }
        public string StartDate { get; set; }
        public string? MeetingPlace { get; set; }
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public int UpazilaId { get; set; }
        public int UnionId { get; set; }
        public int VillageId { get; set; }
        public string? Address { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public DateTime? ClosingDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
    }
}
