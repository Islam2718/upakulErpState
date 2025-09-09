using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.ResponseModel
{
    public class FileStorageResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public List<FileAfterStorageInfo> fileAfterStorageInfos { get; set; }
    }
    public class ResponseStatus
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public FileAfterStorageInfo fileAfterStorageInfo { get; set; }
    }
    public class FileAfterStorageInfo
    {
        public string FileName { get; set; }
        public string FileOrginalLocation { get; set; }
        public string FileLocation { get; set; }
        public string FileExtention { get; set; }
        public string FileType { get; set; }
    }
}
