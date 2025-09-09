using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models.Test
{
    [Table("Test")]
    public class FileUploadTest
    {
        [Key]
        public int Id { get; set; }
        public string Purpose { get; set; }
        public string FileName { get; set; }
        public string FileExtention { get; set; }
        public string Location { get; set; }
        public string OrginalLocation { get; set; }
    }
}
