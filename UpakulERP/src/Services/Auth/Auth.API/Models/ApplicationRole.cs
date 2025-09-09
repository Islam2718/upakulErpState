using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Models
{
    public class ApplicationRole: IdentityRole<int>
    {
        public int ModuleId { get; set; }
    }
}
