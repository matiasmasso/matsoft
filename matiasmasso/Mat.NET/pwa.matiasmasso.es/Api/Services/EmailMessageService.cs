using Api.Entities;
using System.ComponentModel;
using System.Net.Mail;

namespace Api.Services
{
    public class EmailMessageService
    {
        static string host = "smtp.office365.com";
        static string mailFrom = "info@matiasmasso.es";
        static string username = "@info@matiasmasso.es";
        static string password = "Gaho769468";

        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }
        public static void Send(string mailTo, string subject, string body)
        {
            // Command-line argument must be the SMTP host.
            SmtpClient client = new SmtpClient(host);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(username, password);
            client.Port = 25; // You can use Port 25 if 587 is blocked (mine is!)
            client.Host = "smtp.office365.com";
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.EnableSsl = true;

            // Specify the email sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            MailAddress from = new MailAddress(mailFrom);
            // Set destinations for the email message.
            MailAddress to = new MailAddress(mailTo);
            // Specify the message content.
            MailMessage message = new MailMessage(from, to);
            message.Body = body;
            // Include some non-ASCII characters in body and subject.
            message.Body += Environment.NewLine ;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            // Set the method that is called back when the send operation ends.
            client.SendCompleted += new
            SendCompletedEventHandler(SendCompletedCallback);
            // The userState can be any object that allows your callback
            // method to identify this send operation.
            // For this example, the userToken is a string constant.
            string userState = "test message1";
            client.SendAsync(message, userState);
            Console.WriteLine("Sending message... press c to cancel mail. Press any other key to exit.");
            string answer = Console.ReadLine();
            // If the user canceled the send, and mail hasn't been sent yet,
            // then cancel the pending operation.
            if (answer.StartsWith("c") && mailSent == false)
            {
                client.SendAsyncCancel();
            }
            // Clean up.
            message.Dispose();
            Console.WriteLine("Goodbye.");
        }
    }
}
