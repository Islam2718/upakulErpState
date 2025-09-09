using Utility.Domain;

namespace Auth.API.DTO.Response
{
    public class UserXModuleDTOResponse
    {
        public int employeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public List<UserXModule> userXModule { get; set; }
    }
    public class UserXModule
    {
        public int moduleId { get; set; }
        public string moduleName { get; set; }
        public bool isSelected { get; set; }
        public List<CustomSelectListItem> roles { get; set; }
    }
}