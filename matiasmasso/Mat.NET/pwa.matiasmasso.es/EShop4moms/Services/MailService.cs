using DTO;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Shop4moms.Services
{
    public class MailService
    {


        public async static Task SendEmailAsync(string toEmailAddress, string emailSubject, string emailMessage, LangDTO? lang = null)
        {
            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(toEmailAddress));
                message.Subject = emailSubject;
                message.Body = emailMessage;

                if (lang != null && lang.IsPor())
                {
                    message.From = new MailAddress("4moms@matiasmasso.es", "4moms Portugal", Encoding.UTF8);
                    message.Bcc.Add(new MailAddress("4moms@matiasmasso.es", "4moms Portugal", Encoding.UTF8));
                }
                else
                {
                    message.From = new MailAddress("4moms@matiasmasso.es", "4moms España", Encoding.UTF8);
                    message.Bcc.Add(new MailAddress("4moms@matiasmasso.es", "4moms España", Encoding.UTF8));
                }
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

        public static string TemplatedBody(string body)
        {
            //outer template table to horizontally center the body
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<table style='width:100%;'>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td align='center'>");

            //inner template table

            sb.AppendLine("<table style='width:100%;max-width:600px; margin:0 auto;'>");

            //template header
            sb.AppendLine("    <tr style='background-color:black;text-align:center;'>");
            sb.AppendLine("        <td style='text-align:left;padding:10px;'>");
            sb.AppendLine("            <a class='Logo' href='#' style='display:block;'>");
            sb.AppendLine("                <img src='https://4moms.matiasmasso.es/img/logo4moms.jpg' alt='4moms' width='135' height='37' style='height:100%;width:auto;margin:15px;' />");
            sb.AppendLine("            </a>");
            sb.AppendLine("        </td>");
            sb.AppendLine("    </tr>");

            // template body
            sb.AppendLine("    <tr style='min-height:300px;'>");
            sb.AppendLine("        <td style='padding:15px;'>");

            //            <!----start message ---------->

            sb.AppendLine("            <table>");
            sb.AppendLine("                <tr>");
            sb.AppendLine("                    <td style='white-space:pre-line'>");
            sb.AppendLine("                        <text>");

            sb.AppendLine(body);

            sb.AppendLine("                        </text>");
            sb.AppendLine("                    </td>");
            sb.AppendLine("                </tr>");
            sb.AppendLine("            </table>");

            //            <!----end message ---------->


            // end template body
            sb.AppendLine("        </td>");
            sb.AppendLine("    </tr>");

            // template footer
            sb.AppendLine("    <tr style='background-color:black;text-align:center;'>");
            sb.AppendLine("        <td>");
            sb.AppendFormat("            &copy; {0} - Copyright 4moms", @DateTime.Today.Year);
            sb.AppendLine("        </td>");
            sb.AppendLine("    </tr>");

            //end inner template table
            sb.AppendLine("</table>");

            //end outer template table
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }


    }
}
