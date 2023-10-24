using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using FocusAPI.Models;

namespace FocusAPI.Services
{
    public interface IEmailService
    {
        bool Send(EmailRequest emailRequest, CancellationToken ct);
    }
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public bool Send(EmailRequest emailRequest, CancellationToken ct = default)
        {
            try
            {
                // Initialize a new instance of the MimeKit.MimeMessage class
                var mail = new MimeMessage();

                #region Sender / Receiver
                // Sender
                mail.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.From));
                mail.Sender = new MailboxAddress(_emailSettings.DisplayName, _emailSettings.From);

                // Receiver
                mail.To.Add(MailboxAddress.Parse(emailRequest.To));

                // Set Reply to if specified in mail data
                mail.ReplyTo.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.From));
                #endregion

                #region Content

                // Add Content to Mime Message
                var body = new BodyBuilder();
                mail.Subject = emailRequest.Subject;
                body.HtmlBody = emailRequest.Body;
                mail.Body = body.ToMessageBody();

                #endregion

                #region Send Mail

                using var smtp = new SmtpClient();

                if (_emailSettings.UseSSL)
                {
                    smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.SslOnConnect, ct);
                }
                else if (_emailSettings.UseStartTls)
                {
                    smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls, ct);
                }
                smtp.Authenticate(_emailSettings.UserName, _emailSettings.Password, ct);
                smtp.Send(mail, ct);
                smtp.Disconnect(true, ct);

                #endregion

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
