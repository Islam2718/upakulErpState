namespace Auth.API.DTO.Response
{
    public class UsersGridResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeCode { get; set; }
        public string? FullName { get; set; }
        public string OfficeName { get; set; }

    }
    public class UsersGrid
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeCode { get; set; }
        public string? FullName { get; set; }
        public string OfficeName { get; set; }
        public int? TotalCount { get; set; }

    }
}
