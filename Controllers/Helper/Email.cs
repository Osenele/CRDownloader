using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Configuration;

namespace CRDownloader.Controllers.Helper
{
    public class Email
    {

        private static string FromAddress = ConfigurationManager.AppSettings["FromAddress"];
        private static string FromPassword = ConfigurationManager.AppSettings["FromPassword"];
        public static void SendEmail(string emailAddress, string attachmentLink)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(emailAddress))
                {
                    var fromAddress = new MailAddress(FromAddress, "CRDownloader"); //specify email address in WebConfig File
                    var toAddress = new MailAddress(emailAddress);
                    string fromPassword = FromPassword; //specify email password in WebConfig File
                    string subject = "Your Reviews for " + Path.GetFileNameWithoutExtension(attachmentLink);
                    string body = string.Format(@"Hi {0}!<br><br>Find the reviews attached.<br><br>Sent via <b>CR</b>Downloader", emailAddress.Split('@')[0]);
                    Attachment file = new Attachment(attachmentLink);

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        IsBodyHtml = true,
                        Subject = subject,
                        Body = body
                        
                    })
                    {
                        message.Attachments.Add(file);
                        smtp.Send(message);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}