
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace MusicStoreApi.Services
{
    public class EmailService : IEmailService
    {

        public void Send(string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("goodvibez21@yahoo.com"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.mail.yahoo.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("goodvibez21@yahoo.com", "GoodVibez123!");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
