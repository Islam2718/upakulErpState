using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Domain.ViewModels
{
    public class PurposeForGridVM
    {
        public int TotalCount { get; set; }
        public int Id { get; set; }
        public string? Category { get; set; }
        public string? Subcategory { get; set; }
        public string? PurposeCode { get; set; }
        public string PurposeName { get; set; }
        public int MRAPurposeCode { get; set; }
        public string? MRAPurposeName { get; set; }
    }
}
