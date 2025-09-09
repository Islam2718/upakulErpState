using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF.Domain.ViewModels
{
   public class CodeGeneratorVM
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public int CodeLength { get; set; }
        public int StartNo { get; set; }
        public int EndNo { get; set; }
        public string MainJoinCode { get; set; }
        public string VirtualJoinCode { get; set; }
    }
}
