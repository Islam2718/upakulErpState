using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;

namespace MF.Domain.Models
{
    [Table("Group", Schema = "mem")]
    public class Group : EntityBase
    {
        [Key]
        public int GroupId { get; set; }
        public int OfficeId { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string GroupType { get; set; }
        public string ScheduleType { get; set; }
        public int? MeetingDay { get; set; }
        public DateTime OpeninigDate { get; set; }
        public DateTime StartDate { get; set; }
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
        //public string? DocumentURL { get; set; }
    }

}


//public class CustomDateFormatConverter : JsonConverter<DateTime>
//{
//    private const string DateFormat = "yyyy-MM-dd"; // Specify the desired date format

//    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//    {
//        var dateString = reader.GetString();
//        return DateTime.ParseExact(dateString, DateFormat, null);
//    }

//    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
//    {
//        writer.WriteStringValue(value.ToString(DateFormat));
//    }
//}
