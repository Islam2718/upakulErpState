using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HRM.Domain.ViewModels
{
 public  class OfficeTypeXConfigMasterVM
    {
      
        public int OfficeTypeId { get; set; }
        public int ApplicantDesignationId { get; set; }
        public string LeaveCategoryId { get; set; }
        [JsonPropertyName("mappings")]
        public List<OfficeTypeXConfigureDetailsVM> Mappings { get; set; }




    }
}
