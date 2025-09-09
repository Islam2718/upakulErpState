
namespace Accounts.Domain.ViewModel
{
    public class AccountHeadXChildVM
    {
        public int AccountId { get; set; }
        public string HeadCode { get; set; }
        public string HeadName { get; set; }
        public bool IsTransactable { get; set; }
        public int? ParentId { get; set; }
        public List<AccountHeadXChildVM> child { get; set; } = new List<AccountHeadXChildVM>();
    }
}
