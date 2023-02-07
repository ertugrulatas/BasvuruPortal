using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

namespace BasvuruPortal.Service
{
    public class SendEmail
    {
        public static void SendMail(string body, string to, string subject, bool isHtml = true)
        {

            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            try
            {
                MailAddress fromAddress = new MailAddress(WebConfigurationManager.AppSettings["FromMail"], WebConfigurationManager.AppSettings["MailName"]);
                message.From = fromAddress;
                message.To.Add(to);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;

                smtpClient.Host = WebConfigurationManager.AppSettings["MailHost"];
                smtpClient.Port = Convert.ToInt32(WebConfigurationManager.AppSettings["MailPort"]);

                smtpClient.EnableSsl = false;

                smtpClient.Credentials = new NetworkCredential(
                 WebConfigurationManager.AppSettings["FromMail"], WebConfigurationManager.AppSettings["Password"]);

                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}