using BoldReports.Processing.ObjectModel;
using Syncfusion.XPS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BootVerhuurWpf
{
    public class SendEmail
    {
        public static void send(string toEmail)
        {
            try
            {
                //var emailContent = File.ReadAllText("TEMPLATE_FILE_PATH")
                //    .Replace(Login.firstName, "User 001")
                //    .Replace(Login.id, "001")
                //    .Replace(Login.email, "user@mymail.com");

                var smtpClient = new SmtpClient("smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("101ae22b37eceb", "58e0b689fd023e"),
                    EnableSsl = true
                };
                //smtpClient.Host = "smtp.freesmtpservers.com";
                //smtpClient.Port = 25;
                //smtpClient.EnableSsl = true;
                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.Credentials = new NetworkCredential("daan.verweij220@gmail.com", "Dalkruid.13");

                var fromAdress = new MailAddress("07bf10bb37-cb442a+1@inbox.mailtrap.io", "RowHero");
                var toAdress = new MailAddress("test@hotmail.com", "Display name");

                var mailMessage = new MailMessage(fromAdress, toAdress);
                mailMessage.Subject = "RowHero - Wijzing in reservering";
                //mailMessage.Body = emailContent;
                mailMessage.Body = @"Uw reservering is geannuleerd wegens onverwachte onderhoud aan de door u gereserveerde boot. Excuses voor het ongemak.";
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                smtpClient.Send(mailMessage);
                MessageBox.Show("email sent");
            } catch (Exception ex)
            {
                MessageBox.Show($"oeps {ex}");
            }
        }
    }
}
