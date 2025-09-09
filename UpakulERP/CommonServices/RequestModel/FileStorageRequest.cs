using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CommonServices.RequestModel
{
    public class FileStorageRequest
    {
        public int? MaxFileSize { get; set; }
        public string FileTypeAllow {  get; set; }
        public string Location { get; set; }
        public string? EmployeeId {  get; set; }
        //public string Module { get; set; }
        public string? MemberId { get; set; }
        public IFormFile? SingleFile { get; set; }
        public List<IFormFile>? MultipleFile { get; set; }

    }
    
}
