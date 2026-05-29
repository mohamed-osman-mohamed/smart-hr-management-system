using System.Net;
using System.Net.Mail;

namespace PL_Solution.Utilties
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            // Here you can implement the logic to send the email using SMTP or any email service provider.
            var Client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("karemkamon014@gmail.com", "voqgogigninsedfe"),
                EnableSsl = true,
            };
            Client.Send("karemkamon014@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
