namespace Global.Domain.ViewModels
{
    public class OfficeVM
    {
        public int OfficeId { get; set; }
        public int OfficeType { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeShortCode { get; set; }        
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public string? OperationStartDate { get; set; }
        public DateTime? OperationEndDate { get; set; }
        public string? OfficeEmail { get; set; }
        public string? OfficePhoneNo { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public int? ParentId { get; set; }
    }
}
