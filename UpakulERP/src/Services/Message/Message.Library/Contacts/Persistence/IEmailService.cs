using Message.Library.Model;

namespace Message.Library.Contacts.Persistence
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailMessage emailMessage);
    }
}
