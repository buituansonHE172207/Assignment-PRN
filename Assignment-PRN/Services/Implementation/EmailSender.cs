using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using System.Net;
using Microsoft.Extensions.Options;
using Assignment_PRN.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Net.Smtp;

namespace Assignment_PRN.Services.Implementation
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _smtpSettings;

        public EmailSender(IOptions<EmailSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, _smtpSettings.UseSsl);
                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
