
using BasvuruPortal.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BasvuruPortal.BAL
{

    public class MailService
    {

        //public static bool SendMail(string body, string to, string subject, bool isHtml = true)
        //{
        //    return SendMail(body, new List<string> { to }, subject, isHtml);
        //}

        public static void TalepGonder(string subject, string body, bool isHtml = true)
        {
            DatabaseContext db = new DatabaseContext();
            //bool result = false;
          
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
           
            try
            {
                MailAddress fromAddress = new MailAddress("talep@kayseri.edu.tr");
                message.From = fromAddress;
                message.To.Add("talep@kayseri.edu.tr");
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;
                // We use gmail as our smtp client
                smtpClient.Host = "smtp.kayseri.edu.tr";
                smtpClient.Port = 587;
              
                smtpClient.EnableSsl = false;
              
                smtpClient.Credentials = new NetworkCredential(
                 "talep@kayseri.edu.tr", "Talep2021*");

                smtpClient.Send(message);
               
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }

        public static void HataGonder(string subject, string body, bool isHtml = true)
        {
            DatabaseContext db = new DatabaseContext();
            //bool result = false;

            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            try
            {
                MailAddress fromAddress = new MailAddress("yazilim@kayseri.edu.tr");
                message.From = fromAddress;
                message.To.Add("yazilim@kayseri.edu.tr");
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;
                // We use gmail as our smtp client
                smtpClient.Host = "smtp.kayseri.edu.tr";
                smtpClient.Port = 25;

                smtpClient.EnableSsl = true;

                smtpClient.Credentials = new NetworkCredential(
                 "yazilim@kayseri.edu.tr", "Kayu.Soft*2000");

                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public static void bildirimmail(string mail, string subject, string body, bool isHtml = true)
        {
            DatabaseContext db = new DatabaseContext();
            //bool result = false;

            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            try
            {
                MailAddress fromAddress = new MailAddress("yazilim@kayseri.edu.tr");
                message.From = fromAddress;
                message.To.Add(mail);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;
                // We use gmail as our smtp client
                smtpClient.Host = "smtp.kayseri.edu.tr";
                smtpClient.Port = 587;

                smtpClient.EnableSsl = false;

                smtpClient.Credentials = new NetworkCredential(
                 "yazilim@kayseri.edu.tr", "Kayu.Soft*2000");

                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
