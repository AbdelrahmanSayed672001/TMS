using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;


namespace TMS_V1.Services
{
    /// <summary>
    /// A service for sending email messages using the configured SMTP server.
    /// Implements the <see cref="IEmailSender"/> interface.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Sends an email asynchronously using the configured SMTP settings.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="htmlMessage">The HTML body of the email.</param>
        /// <returns>A task representing the asynchronous email sending operation.</returns>
        /// <exception cref="Exception">Thrown if there is an issue connecting or sending the email.</exception>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fromMail = _configuration["EmailSenderSettings:FromMail"];
            var fromPassword = _configuration["EmailSenderSettings:FromPassword"];
            var host = _configuration["EmailSenderSettings:Host"];
            var port = int.Parse(_configuration["EmailSenderSettings:Port"]);
            var displayName = "TMS";

            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(fromMail),
                Subject = subject
            };

            mail.To.Add(MailboxAddress.Parse(email));
            var builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;
            mail.Body = builder.ToMessageBody();
            mail.From.Add(new MailboxAddress(displayName, fromMail));
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
                smtp.Authenticate(fromMail, fromPassword);
                await smtp.SendAsync(mail);
                smtp.Disconnect(true);
            }


        }
    }
}

