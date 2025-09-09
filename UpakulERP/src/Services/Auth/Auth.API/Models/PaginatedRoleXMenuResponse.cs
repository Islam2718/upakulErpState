namespace Auth.API.Models
{
    public class PaginatedRoleXMenuResponse
    {
        public List<RoleXMenu> listData { get; set; }
        public int TotalRecords { get; set; }

        public PaginatedRoleXMenuResponse(List<RoleXMenu> obj, int totalRecords)
        {
            listData = obj;
            TotalRecords = totalRecords;
        }
    }
}
