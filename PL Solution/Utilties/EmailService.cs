using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace PL_Solution.Utilties
{
    public class EmailService(IOptions<MailSettings> _options) : IEmailService
    {
        public void SendEmail(Email email)
        {
            //1.Build the email message using the provided email object
            var mail = new MimeMessage();
                mail.Sender = MailboxAddress.Parse(_options.Value.Email);
                mail.From.Add(new MailboxAddress(_options.Value.DisplayName, _options.Value.Email));
                mail.To.Add(MailboxAddress.Parse(email.To));
                mail.Subject = email.Subject;
            // For the email body, we can use the BodyBuilder class to create a MIME body from the HTML content
            var builder = new BodyBuilder { HtmlBody = email.Body };
                mail.Body = builder.ToMessageBody();
            //2.Establish a connection to the email server using the settings from _options
            using var smtp = new SmtpClient(); // opend Channel
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.Connect(_options.Value.Host, _options.Value.Port, MailKit.Security.SecureSocketOptions.StartTls); // connect to the server
                smtp.Authenticate(_options.Value.Email, _options.Value.Password);// authenticate with the server using the provided credentials
            //3.Send the email message
                smtp.Send(mail);
                smtp.Dispose(); // close the connection 
        }
    }
}
