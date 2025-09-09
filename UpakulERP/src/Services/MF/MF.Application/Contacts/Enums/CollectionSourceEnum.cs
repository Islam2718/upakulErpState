
using System.ComponentModel.DataAnnotations;

namespace MF.Application.Contacts.Enums
{
    public enum CollectionSourceEnum
    {
        Web = 'W',
        [Display(Name = "Mobile App")]
        Mobile_App = 'I',
        [Display(Name = "Mobile Financial Services")]
        MFs ='M',
        Bank='B'
    }
}
