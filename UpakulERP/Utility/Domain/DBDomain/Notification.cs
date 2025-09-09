namespace Utility.Domain.DBDomain
{
    public class Notification
    {
        public int count { get; set; }
        public List<NotificationModel> summary { get; set; }
    }

    public class NotificationModel
    {
        public string NotificationType { get; set; }
        public string? LoanType { get; set; }
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Office { get; set; }
        public string? Group { get; set; }
        public string? GraceFrom { get; set; }
        public string? GraceTo { get; set; }
        public string? ApplicationDate { get; set; }
        public int? ProposedAmount { get; set; }
        public DateTime? OrderBy { get; set; }
    }
}
