using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApp_WithUsers.Services
{

    public class AuthMessageSenderOptions
    {
        public string SmtpUser => "noreply@mail.ru";
        public string SmtpKey => "password";
    }

    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor) =>
            Options = optionsAccessor.Value;

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager
        public Task SendEmailAsync(string email, string subject, string message) => 
            Execute(subject, message, email);
        public Task SendEmailAsync(string email, string subject, string message, Attachment attach) => 
            Execute(subject, message, email, attach);
        public Task Execute(string subject, string message, string email, Attachment attach = null)
        {
            SmtpClient client = new SmtpClient("mail.ru");
            client.Credentials = new System.Net.NetworkCredential(Options.SmtpUser, Options.SmtpKey);
            MailAddress from = new MailAddress("noreply@mail.ru", "Портал...", System.Text.Encoding.UTF8);
            // Set destinations for the email message.
            MailAddress to = new MailAddress(email);
            // Specify the message content.
            MailMessage mmessage = new MailMessage(from, to);
            mmessage.IsBodyHtml = true;
            mmessage.Body = message;
            mmessage.Subject = subject;
            mmessage.SubjectEncoding = System.Text.Encoding.UTF8;
            if (attach != null)
                mmessage.Attachments.Add(attach);

            string userState = Guid.NewGuid().ToString();
            return client.SendMailAsync(mmessage);

        }
    }
}
