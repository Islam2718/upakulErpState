using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Message.Domain.Models
{
    [Table("UserLog",Schema ="email")]
    public class UserMailLog
    {
        [Key]
       public long Id   {get;set;}
       public string? UserId { get;set;}
       public string? MailTo { get;set;}
       public string? MailBody { get;set;}
       public string? SenderUser { get;set;}
       public string? Purpus { get;set;}
       public DateTime SendDate { get;set;}=DateTime.Now;
        public bool IsSend { get;set;}=false;
    }
}
