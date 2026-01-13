using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Shop4moms.Services
{
    public class MailService
    {


        public async static Task SendEmailAsync(string toEmailAddress, string emailSubject, string emailMessage)
        {
            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(toEmailAddress));
                message.Subject = emailSubject;
                message.Body = emailMessage;
                message.From = new MailAddress("info@matiasmasso.es", "M+O", Encoding.UTF8);
                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                message.BodyTransferEncoding = TransferEncoding.Base64;


                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Host = "smtp.office365.com";
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Port = 25;
                    smtpClient.Credentials = new NetworkCredential("info@matiasmasso.es", "Tan76831");
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await smtpClient.SendMailAsync(message);
                }
            }
        }

    }
}
