
namespace MF.Domain.ViewModels
{
    public class OfficeComponentMappingVM
    {
        public int OfficeComponentMappingId { get; set; }
        public int ComponentId { get; set; }
        public int OfficeId { get; set; }
        public List<string> SelectedBranch { get; set; }

    }
}
