namespace MF.Domain.ViewModels
{
    public class SamityVM
    {
        public int GroupId { get; set; }
        public int OfficeId { get; set; }
        public string? GroupName { get; set; }
        public string? GroupCode { get; set; }
        public string? GroupType { get; set; }
        public string ScheduleType { get; set; }
        public DateTime OpeninigDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public int MeetingDay { get; set; }
        public string? MeetingPlace { get; set; }
        //public int? SamityLeaderMemberId { get; set; }
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public int UpazilaId { get; set; }
        public int UnionId { get; set; }
        public int VillageId { get; set; }
        public string? Address { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        //public string? DocumentURL { get; set; }
    }
}
