using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Library.Model
{
    public class EmailMessage
    {
        public string To { get; set; }
        public string? ToDisplayName { get; set; }
        public string? CC { get; set; }
        public string? CCDisplayName { get; set; }
        public string? Bcc { get; set; }
        public string? BccDisplayName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FilePath { get; set; }
    }
}
