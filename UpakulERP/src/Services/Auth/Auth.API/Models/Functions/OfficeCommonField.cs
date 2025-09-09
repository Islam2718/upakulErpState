using System.ComponentModel.DataAnnotations;

namespace Auth.API.Models.Functions
{
    public class OfficeCommonField
    {
        public int? OfficeId { get; set; }
        public string? OfficeCode { get; set; }
        public string? OfficeName { get; set; }
       
    }
}
