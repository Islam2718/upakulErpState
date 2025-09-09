using System.Net;
using Message.Library.Model;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Message.Library.Contacts.Repository
{
    public class EmailService
    {
        public async Task<bool> SendEmailAsync(EmailMessage emailMessage)
        {
            try
            {
                if (IsValidEmail(emailMessage.To))
                {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("System of COAST Foundation", "system@coastbd.net"));

                    message.To.Add(new MailboxAddress((!string.IsNullOrEmpty(emailMessage.ToDisplayName) ? emailMessage.ToDisplayName : ""), emailMessage.To));
                    if (IsValidEmail(emailMessage.CC ?? ""))
                        message.Cc.Add(new MailboxAddress((!string.IsNullOrEmpty(emailMessage.CCDisplayName) ? emailMessage.CCDisplayName : ""), emailMessage.CC));
                    if (IsValidEmail(emailMessage.Bcc ?? ""))
                        message.Bcc.Add(new MailboxAddress((!string.IsNullOrEmpty(emailMessage.BccDisplayName) ? emailMessage.BccDisplayName : ""), emailMessage.Bcc));
                    message.Subject = emailMessage.Subject;

                    // Create the email body
                    var builder = new BodyBuilder
                    {
                        HtmlBody = emailMessage.Body
                        //TextBody = "Please find the file attached.",
                    };
                    //builder.LinkedResources.Add()

                    // 📎 Attach a file (full path or relative path)
                    if (!string.IsNullOrEmpty(emailMessage.FilePath))
                        if (File.Exists(emailMessage.FilePath))
                            builder.Attachments.Add(emailMessage.FilePath);

                    // Set the message body
                    message.Body = builder.ToMessageBody();

                    using var client = new SmtpClient();
                    //await client.ConnectAsync("smtp.coastbd.net", 587, SecureSocketOptions.StartTls);
                    await client.ConnectAsync("mail.coastbd.net", 25, SecureSocketOptions.StartTls);
                    //await client.ConnectAsync("smtp.coastbd.net", 465, SecureSocketOptions.SslOnConnect);
                    await client.AuthenticateAsync("system@coastbd.net", "kjiasep8qewbnck%#$");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                    return true;
                }

                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SendEmail(EmailMessage emailMessage)
        {
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.From = new System.Net.Mail.MailAddress("system@coastbd.net", "System of COAST Foundation");
                if (IsValidEmail(emailMessage.To))
                {
                    mail.To.Add(new System.Net.Mail.MailAddress(emailMessage.To, (!string.IsNullOrEmpty(emailMessage.ToDisplayName) ? emailMessage.ToDisplayName : "")));
                    if (IsValidEmail(emailMessage.CC))
                        mail.CC.Add(new System.Net.Mail.MailAddress(emailMessage.CC, (!string.IsNullOrEmpty(emailMessage.CCDisplayName) ? emailMessage.CCDisplayName : "")));
                    if (IsValidEmail(emailMessage.Bcc))
                        mail.Bcc.Add(new System.Net.Mail.MailAddress(emailMessage.Bcc, (!string.IsNullOrEmpty(emailMessage.BccDisplayName) ? emailMessage.BccDisplayName : "")));
                    mail.Subject = emailMessage.Subject;
                    mail.Body = emailMessage.Body;
                    mail.IsBodyHtml = true;
                    System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("smtp.coastbd.net", 587)
                    {
                        Credentials = new NetworkCredential("system@coastbd.net", "kjiasep8qewbnck%#$"),
                        EnableSsl = true
                    };

                    smtpClient.Send(mail);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }




        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
                return false;

            try
            {
                // Use MailAddress for format validation
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
