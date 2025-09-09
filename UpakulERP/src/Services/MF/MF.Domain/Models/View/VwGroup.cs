using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Utility.Enums;

namespace MF.Domain.Models.View
{
    [Table("vw_Group", Schema = "mem")]
    public class VwGroup
    {
        public int GroupId { get; set; }
        public int OfficeId { get; set; }
        public string Office { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string? SamityTypeName { get; set; }
        public string? MeetingDayName { get; set; }
        //public string? GroupLeaderMember { get; set; }
        public DateTime OpeninigDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string? MeetingPlace { get; set; }
        public string? Division { get; set; }
        public string? District { get; set; }
        public string? Upazila { get; set; }
        public string? Union { get; set; }
        public string? Village { get; set; }
        public string? Address { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

    }
}
