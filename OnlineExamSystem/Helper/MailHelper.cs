using System;
using System.Net;
using System.Net.Mail;

namespace OnlineExamSystem.Helper
{
    public class MailHelper
    {
        public static bool SendVerificationEmail(string toEmail, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("Edu.paarth@gmail.com", " PAARTH INSTITUTE Online Exam System");
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                string senderEmail =
                    ConfigurationManager.AppSettings["SenderEmail"];

                string senderPassword =
                    ConfigurationManager.AppSettings["SenderPassword"];


                SmtpClient smtp = new SmtpClient(senderEmail, senderPassword);
                smtp.EnableSsl = true;

                smtp.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}