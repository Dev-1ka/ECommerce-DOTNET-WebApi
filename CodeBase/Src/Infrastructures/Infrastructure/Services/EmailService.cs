using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var emailAddress = _config["EmailSettings:Email"];
            var password = _config["EmailSettings:Password"];
            var host = _config["EmailSettings:Host"];
            var port = int.Parse(_config["EmailSettings:Port"]);

            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(emailAddress));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            email.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(host, port, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(emailAddress, password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
